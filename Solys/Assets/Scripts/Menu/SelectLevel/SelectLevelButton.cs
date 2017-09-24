using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelButton : MonoBehaviour {

	public int id;
    public GameObject StarsObject;

    public void Start()
    {
        SetStarsForButton(PrefsDriver.GetStarsForLevel(id));
    }

    public void OnClick()
	{

        SceneManager.LoadScene(id);
	}


    private void SetStarsForButton(int stars)
    {
        for (int i = 0; i < stars; i++)
            StarsObject.transform.GetChild(i).gameObject.SetActive(true);

    }
}
