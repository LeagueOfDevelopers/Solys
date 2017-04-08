using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectLevelMenuScript : MonoBehaviour {

	public GameObject Content;
	public GameObject ButtonTemplate;
	public int SceneAmount;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		for(int i = 1; i<=SceneAmount;i++)
		{
			GameObject button = Instantiate(ButtonTemplate,Content.transform);
			button.transform.localScale = new Vector3(1,1,1);
			button.transform.FindChild("Text").GetComponent<Text>().text = (i).ToString();
			button.GetComponent<SelectLevelButton>().id = i;
		}
		
	}

	public void BackButtonClick()
	{
		SceneManager.LoadScene("Menu");
	}
	
}
