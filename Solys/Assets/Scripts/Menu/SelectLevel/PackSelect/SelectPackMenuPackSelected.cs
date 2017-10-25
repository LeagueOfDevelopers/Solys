using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectPackMenuPackSelected : MonoBehaviour {

    public void PackSelect(string PackTitle, int FirstScene, int LastScene)
    {
        SceneDataTransfer.Instance.FirstLevelInPack = FirstScene;
        SceneDataTransfer.Instance.LastLevelInPack = LastScene;
        SceneDataTransfer.Instance.PackTitle = PackTitle;
        GetComponent<Animator>().SetBool("Exit", true);
    }


    public void AnimationEnd()
    {
        SceneManager.LoadScene("SelectLevelMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("0");
    }
}
