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

        GameObject.Find("Scroll View").GetComponent<LevelSelectAnim>().OpenScene(id);
    }

    public void SetTitle(string title)
    {
        Title.GetComponent<Text>().text = title;
    }

    private void SetStarsForButton(int stars)
    {
        StarsObject.GetComponent<StarsHandler>().SetStars(stars);

    }

    private void AnimSetStarsForButton(int stars)
    {

    }

}
