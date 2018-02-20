using UnityEngine.SceneManagement;
using UnityEngine;

public class OpenShareScene : MonoBehaviour {

	public void OnClick()
    {
        SceneManager.LoadScene("Share");
    }
}
