using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SelectLevelMenuScript : MonoBehaviour {


	public GameObject Content;
	public GameObject ButtonTemplate;

	public GameObject Title;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		ClearContent();
		SetPackTitle();
		FillContentWithButtons();
		
	}

	public void BackButtonClick()
	{
		SceneManager.LoadScene("Menu");
	}

	private void ClearContent()
	{
		for (int i = 0; i<Content.transform.childCount; i++)
		{
			Destroy(Content.transform.GetChild(i).gameObject);
		}
	}

	private void FillContentWithButtons()
	{
		int sceneIndex = 1;

		for(int i = SceneDataTransfer.Instance.FirstLevelInPack; i<=SceneDataTransfer.Instance.LastLevelInPack;i++)
		{
			GameObject button = Instantiate(ButtonTemplate,Content.transform);
			button.transform.localScale = new Vector3(1,1,1);
			button.GetComponent<SelectLevelButton>().SetTitle(sceneIndex.ToString());
			button.GetComponent<SelectLevelButton>().id = i;
            sceneIndex++;
		}
	}

	private void SetPackTitle()
	{
		Title.GetComponent<Text>().text = SceneDataTransfer.Instance.PackTitle;
	}
	
}
