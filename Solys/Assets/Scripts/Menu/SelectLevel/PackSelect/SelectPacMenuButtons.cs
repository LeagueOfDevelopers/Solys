using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectPacMenuButtons : MonoBehaviour {

	public void HomeClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenSelectPackScene()
    {
        SceneManager.LoadScene(1);
    }
}
