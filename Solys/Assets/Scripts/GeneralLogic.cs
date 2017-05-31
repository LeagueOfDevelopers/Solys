using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralLogic : MonoBehaviour {

	public delegate void Action();
    public GameObject NextLevelButton;
    public GameObject ExitButton;
    public GameObject[] StarsImages;
    public static Action StartSimulationEvent;
	public static Action StopSimulationEvent;
	public static Action ResetSimulationEvent;
    public GameObject LineWriterObject;

	public static int SnapValueForMapElements = 1;	///TODO move away from this file!

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		TargetLogic.TargetReached += TargetReached;
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		TargetLogic.TargetReached -= TargetReached;
	}
	public void StartSimulation()
	{
		Debug.Log("START");
		if(StartSimulationEvent != null)
			StartSimulationEvent();
	}

	public void StopSimulation()
	{
		if(StopSimulationEvent != null)
			StopSimulationEvent();
	}

	public void ResetSimulation()
	{
		Debug.Log("RESET");
		if(ResetSimulationEvent != null)
			ResetSimulationEvent();
	}

	private void TargetReached()
	{
		StopSimulation();
		OpenEndLevelMenu();
	}

	private void OpenEndLevelMenu()
	{
        int stars = UpdateLevelRating();
        NextLevelButton.SetActive(true);
        ExitButton.SetActive(true);
        for (int i = 0; i < stars; i++)
        {
            StarsImages[i].SetActive(true);
        }
    }

    private int UpdateLevelRating()
    {
        float procent1 = 0.3f;
        float procent2 = 0.6f;
        int stars=0;
        float currentProcent = LineWriterObject.GetComponent<LineWriter>().LineRemainder;
        if (currentProcent > procent2) stars = 3;
        else
        if (currentProcent < procent1) stars = 1;
        else stars = 2;
        SceneDataTransfer.Instance.currentRate = stars;
        return stars;
    }

    public void NextLevelButtonClick(int scene)
    {
        Debug.Log("Next Level selected");
		int LevelID = SceneDataTransfer.Instance.CurrentSceneID;
		Debug.Log(LevelID);
        //SceneManager.LoadScene(scene);
		if(SceneManager.sceneCountInBuildSettings>LevelID+1)
		{
			SceneDataTransfer.Instance.CurrentSceneID = LevelID+1;
			SceneManager.LoadScene(LevelID+1);
		}
		else
			SceneManager.LoadScene("Menu");
    }

    public void ExitButtonClick()
    {
       Debug.Log("Next Level selected");
        SceneManager.LoadScene(0);
    }
    
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Menu");
	}
}
