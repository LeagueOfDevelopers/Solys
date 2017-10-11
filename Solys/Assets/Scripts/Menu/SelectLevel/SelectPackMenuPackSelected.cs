using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectPackMenuPackSelected : MonoBehaviour {

    public void PackSelect(int FirstScene, int LastScene)
    {
        SceneDataTransfer.Instance.FirstLevelInPack = FirstScene;
        SceneDataTransfer.Instance.LastLevelInPack = LastScene;
        GetComponent<Animator>().SetBool("Exit", true);
    }


    public void AnimationEnd()
    {
        SceneManager.LoadScene("SelectLevelMenu");
    }
}
