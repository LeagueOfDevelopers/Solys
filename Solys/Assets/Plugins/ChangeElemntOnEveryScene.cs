#if (UNITY_EDITOR) 

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;


public class ChangeElemntOnEveryScene : EditorWindow {

    private string currentScenePath;

    private string objectName;
    private GameObject prefabToChange;
    private int startSceneIndex;
    private bool isNeedToChangeCanvasCamera;
    private string[] deleteThisObjects;

    private GameObject UI;
    private GameObject LineWriter;

    private void OnEnable()
    {
        deleteThisObjects = new string[5];
    }

    [MenuItem("Window/Custom Editor/Change Element On Every Scene")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ChangeElemntOnEveryScene));
    }

    void OnGUI()
    {
        GUILayout.Label("Change Object to prefab for every scene in build", EditorStyles.boldLabel);
        objectName = EditorGUILayout.TextField("Object Name", objectName);
        prefabToChange = (GameObject)EditorGUILayout.ObjectField("Prefab",prefabToChange, typeof(GameObject), true);
        startSceneIndex = EditorGUILayout.IntField("Start Scene Build Index", startSceneIndex);
        isNeedToChangeCanvasCamera = EditorGUILayout.Toggle("Is it For Main camera?", isNeedToChangeCanvasCamera);
        if (GUILayout.Button("Start!") && objectName != null && prefabToChange != null)
            ChangeObjects();
        if (GUILayout.Button("Delete Missings!"))
            DeleteAllMissingPrefabs();

        
        GUILayout.Label("Delete up to 5 objects on every scene", EditorStyles.boldLabel);
        for (int i = 0; i <deleteThisObjects.Length;i++)
            deleteThisObjects[i] = EditorGUILayout.TextField(i.ToString(), deleteThisObjects[i]);

        if (GUILayout.Button("Delete THAT SHIT!"))
            DeleteObjectsOnEverySceneByName();

        GUILayout.Label("Add LineWriter And UI", EditorStyles.boldLabel);
        UI = (GameObject)EditorGUILayout.ObjectField("UI", UI, typeof(GameObject), true);
        LineWriter = (GameObject)EditorGUILayout.ObjectField("LW", LineWriter, typeof(GameObject), true);

        if (GUILayout.Button("Replace UI and LW!"))
            AddUIandLWToAllScenes();

    }

    void ChangeObjects()
    {
        currentScenePath = EditorSceneManager.GetActiveScene().path;
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        
        for(int i = startSceneIndex; i<EditorSceneManager.sceneCountInBuildSettings; i++)
        {
            
            string path = EditorBuildSettings.scenes[i].path;
            UnityEngine.SceneManagement.Scene openedScene = EditorSceneManager.OpenScene(path);

            Debug.Log("Changind " + path);
            ChangeObjectOnScene();
            
            EditorSceneManager.SaveScene(openedScene);

        }
        


    }

    private void ChangeObjectOnScene()
    {
        GameObject camera = GameObject.Find(objectName);

        if(camera == null)
        {
            Debug.Log("No OBJECT here -----------------" + EditorSceneManager.GetActiveScene().path);
            return;
        }
        
        GameObject newCamera = Instantiate(prefabToChange, camera.transform.parent);
        newCamera.name = prefabToChange.name;
        newCamera.transform.position = camera.transform.position;
        PrefabUtility.ConnectGameObjectToPrefab(newCamera, prefabToChange);
        DestroyImmediate(camera);
        if(isNeedToChangeCanvasCamera)
        {
            GameObject.Find("Canvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            GameObject.Find("Canvas").GetComponent<Canvas>().worldCamera = newCamera.GetComponent<Camera>();
            GameObject.Find("Canvas").GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            GameObject.Find("Canvas").GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
    }


    private void DeleteAllMissingPrefabs()
    {
        currentScenePath = EditorSceneManager.GetActiveScene().path;
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

        for (int i = startSceneIndex; i < EditorSceneManager.sceneCountInBuildSettings; i++)
        {

            string path = EditorBuildSettings.scenes[i].path;
            UnityEngine.SceneManagement.Scene openedScene = EditorSceneManager.OpenScene(path);

            Debug.Log("Delete missing " + path);
            DeleteMissingPrefabsOnScene();

            EditorSceneManager.SaveScene(openedScene);

        }
    }

    private void DeleteMissingPrefabsOnScene()
    {
        GameObject missing;
        missing = GameObject.Find("Missing Prefab");
        do
        {
            if (missing == null) return;
            Debug.Log(missing.name + EditorSceneManager.GetActiveScene().path);
            DestroyImmediate(missing);
            missing = GameObject.Find("Missing Prefab");
        } while (missing!=null);
    }
    
    private void DeleteObjectsOnEverySceneByName()
    {
        currentScenePath = EditorSceneManager.GetActiveScene().path;
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

        for (int i = startSceneIndex; i < EditorSceneManager.sceneCountInBuildSettings; i++)
        {

            string path = EditorBuildSettings.scenes[i].path;
            UnityEngine.SceneManagement.Scene openedScene = EditorSceneManager.OpenScene(path);

            foreach (string name in deleteThisObjects)
            {
                
                Debug.Log("Delete Element " + name +" "+ path);
                DestroyImmediate(GameObject.Find(name));

            }
            EditorSceneManager.SaveScene(openedScene);

        }
    }

    
    private void AddUIandLWToAllScenes()
    {
        currentScenePath = EditorSceneManager.GetActiveScene().path;
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

        for (int i = startSceneIndex; i < EditorSceneManager.sceneCountInBuildSettings; i++)
        {

            string path = EditorBuildSettings.scenes[i].path;
            UnityEngine.SceneManagement.Scene openedScene = EditorSceneManager.OpenScene(path);

            Debug.Log("UI and LW " + path);
            AddUIandLWTOTHisScene();
            
            EditorSceneManager.SaveScene(openedScene);

        }
    }

    private void AddUIandLWTOTHisScene()
    {
        GameObject canvas = GameObject.Find("Canvas");
        DestroyImmediate(GameObject.Find("UI"));
        DestroyImmediate(GameObject.Find("LineWriter"));
        DestroyImmediate(GameObject.Find("UI(Clone)"));
        DestroyImmediate(GameObject.Find("LineWriter(Clone)"));
        GameObject newLW = (GameObject)Instantiate(LineWriter);
        GameObject newUI = (GameObject)Instantiate(UI, canvas.transform, false);
        newLW.name = "LineWriter";
        newUI.name = "UI";
        PrefabUtility.ConnectGameObjectToPrefab(newLW, LineWriter);
        PrefabUtility.ConnectGameObjectToPrefab(newUI, UI);
    }
}
#endif