using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectPackMenuPackSelected : MonoBehaviour {

    private bool isPackRoute = true;

    public void PackSelect(string PackTitle, int FirstScene, int LastScene)
    {
        SceneDataTransfer.Instance.FirstLevelInPack = FirstScene;
        SceneDataTransfer.Instance.LastLevelInPack = LastScene;
        SceneDataTransfer.Instance.PackTitle = PackTitle;
        isPackRoute = true;
        GetComponent<Animator>().SetBool("Exit", true);
        WriteContentScrollPos();
    }

    private void WriteContentScrollPos()
    {
        GameObject obj = transform.FindChild("Viewport").FindChild("Content").gameObject;
        PrefsDriver.SetScrollPosForPackSelect(obj.GetComponent<RectTransform>().localPosition.x);
    }

    public void AnimationEnd()
    {
        if(isPackRoute)
            SceneManager.LoadScene("SelectLevelMenu");
        else
            SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPackRoute = false;
            GetComponent<Animator>().SetBool("Exit", true);
        }
    }
}
