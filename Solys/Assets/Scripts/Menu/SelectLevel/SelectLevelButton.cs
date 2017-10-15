using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour {

	public int id;
    public GameObject StarsObject;
    public GameObject Title;

    public void Start()
    {
        SetStarsForButton(PrefsDriver.GetStarsForLevel(id));
    }

    public void OnClick()
	{

        SceneManager.LoadScene(id);
	}

    public void SetTitle(string title)
    {
        Title.GetComponent<Text>().text = title;
    }

    private void SetStarsForButton(int stars)
    {
        for (int i = 0; i < stars; i++)
            StarsObject.transform.GetChild(i).gameObject.SetActive(true);

    }

}
