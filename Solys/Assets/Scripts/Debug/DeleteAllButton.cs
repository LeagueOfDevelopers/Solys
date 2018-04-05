using UnityEngine.SceneManagement;
using UnityEngine;

public class DeleteAllButton : MonoBehaviour {

	public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
