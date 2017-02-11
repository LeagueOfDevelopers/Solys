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


	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		TargetLogic.TargetReached += TargetReched;
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		TargetLogic.TargetReached -= TargetReched;
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

	private void TargetReched()
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
        SceneManager.LoadScene(scene);
    }

    public void ExitButtonClick()
    {
       Debug.Log("Next Level selected");
        SceneManager.LoadScene(0);
    }
    
}
