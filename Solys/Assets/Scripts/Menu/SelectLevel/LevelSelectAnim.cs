using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelectAnim : MonoBehaviour {

    private int sceneToOpen;

	private void AnimationEnd()
    {
        SceneManager.LoadScene(sceneToOpen);
    }

    public void OpenScene(int scene)
    {
        GetComponent<Animator>().SetBool("Exit", true);
        sceneToOpen = scene;
    }
}
