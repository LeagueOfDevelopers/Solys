using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SelectLevelMenuScript : MonoBehaviour {

	public GameObject Content;
	public GameObject ButtonTemplate;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
        int sceneIndex = 1;

		for(int i = SceneDataTransfer.Instance.FirstLevelInPack; i<=SceneDataTransfer.Instance.LastLevelInPack;i++)
		{
			GameObject button = Instantiate(ButtonTemplate,Content.transform);
			button.transform.localScale = new Vector3(1,1,1);
			button.transform.FindChild("Text").GetComponent<Text>().text = sceneIndex.ToString();
			button.GetComponent<SelectLevelButton>().id = i;
            sceneIndex++;
		}
		
	}

	public void BackButtonClick()
	{
		SceneManager.LoadScene("Menu");
	}
	
}
