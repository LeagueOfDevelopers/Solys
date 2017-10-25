using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ChangeElemntOnEveryScene : EditorWindow {

    private string currentScenePath;

    private string objectName;
    private GameObject prefabToChange;
    private int startSceneIndex;
    private bool isNeedToChangeCanvasCamera;

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
        DestroyImmediate(camera);
        GameObject newCamera = Instantiate(prefabToChange);
        newCamera.name = objectName;

        if(isNeedToChangeCanvasCamera)
        {
            GameObject.Find("Canvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            GameObject.Find("Canvas").GetComponent<Canvas>().worldCamera = newCamera.GetComponent<Camera>();
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
            
            Debug.Log(missing.name + EditorSceneManager.GetActiveScene().path);
            DestroyImmediate(missing);
            missing = GameObject.Find("Missing Prefab");
        } while (missing!=null);
    }
}
