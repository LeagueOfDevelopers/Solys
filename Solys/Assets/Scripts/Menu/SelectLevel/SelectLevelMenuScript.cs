using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectLevelMenuScript : MonoBehaviour {

	public GameObject Content;
	public GameObject ButtonTemplate;
	public int FirstSceneId;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		for(int i = FirstSceneId; i<SceneManager.sceneCountInBuildSettings;i++)
		{
			GameObject button = Instantiate(ButtonTemplate,Content.transform);
			Scene scene = SceneManager.GetSceneByBuildIndex(i);
			button.transform.localScale = new Vector3(1,1,1);
			button.transform.FindChild("Text").GetComponent<Text>().text = (i-FirstSceneId+1).ToString();
			button.GetComponent<SelectLevelButton>().id = i;
		}
		
	}

	public void BackButtonClick()
	{
		SceneManager.LoadScene("Menu");
	}
	
}
