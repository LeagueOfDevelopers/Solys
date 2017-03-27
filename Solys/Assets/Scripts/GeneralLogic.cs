using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralLogic : MonoBehaviour {

	public delegate void Action();
    public GameObject NextLevelButton;
    public GameObject ExitButton;
	public static Action StartSimulationEvent;
	public static Action StopSimulationEvent;
	public static Action ResetSimulationEvent;

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
	   
        NextLevelButton.SetActive(true);
        ExitButton.SetActive(true);
	}

    public void NextLevelButtonClick(int scene)
    {
        Debug.Log("Next Level selected");
		int LevelID = PlayerPrefs.GetInt("LevelID",-1);
		Debug.Log(LevelID);
        //SceneManager.LoadScene(scene);
		if(SceneManager.sceneCountInBuildSettings>LevelID+1)
		{
			PlayerPrefs.SetInt("LevelID",LevelID+1);
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
