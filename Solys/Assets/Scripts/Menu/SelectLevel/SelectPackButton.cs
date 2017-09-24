using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectPackButton : MonoBehaviour {

    public int FirstScene;
    public int LastScene;

    public void onClick()
    {
        SceneDataTransfer.Instance.FirstLevelInPack = FirstScene;
        SceneDataTransfer.Instance.LastLevelInPack = LastScene;
        SceneManager.LoadScene("SelectLevelMenu");
    }
}
