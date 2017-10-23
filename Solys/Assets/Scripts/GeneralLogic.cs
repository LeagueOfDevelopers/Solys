using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralLogic : MonoBehaviour {

	public delegate void Action();
    public static Action StartSimulationEvent;
	public static Action StopSimulationEvent;
	public static Action ResetSimulationEvent;
    public static Action PauseBlockActivatedEvent;
    public GameObject LineWriterObject;

	public GameObject ui;
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

    public void PauseBlockActivated()
    {
        if (PauseBlockActivatedEvent != null)
        {
            PauseBlockActivatedEvent();
            StopSimulation();
        }
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
        UpdateLevelRating();
        ui.GetComponent<UIHandlerScript>().TargetReached();
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
        PrefsDriver.SetStarsForLevel(SceneManager.GetActiveScene().buildIndex, stars);
		SceneDataTransfer.Instance.NeedToUpdateStarsForLevel = SceneManager.GetActiveScene().buildIndex;
        SceneDataTransfer.Instance.LastLevelRating = stars;
        Debug.Log(SceneManager.GetActiveScene().buildIndex.ToString());
        return stars;
    }

   
    
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("2");
	}
}
