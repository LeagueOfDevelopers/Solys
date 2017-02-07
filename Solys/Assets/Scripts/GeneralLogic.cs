using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralLogic : MonoBehaviour {

	public delegate void Action();
	public static Action StartSimulationEvent;
	public static Action StopSimulationEvent;
	public static Action ResetSimulationEvent;
    private bool ShowMenu = false;


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
	    ShowMenu = true;
	}

    void OnGUI()
    {
        if (ShowMenu)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 70));
            GUILayout.BeginVertical();
            if (GUILayout.Button("Начать игру"))
            {
               Debug.Log("Next Level selected");
                ShowMenu = false;
                Application.LoadLevel("TestScene");
            }
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button("Выход"))
            {
                Debug.Log("Exit selected");
                Application.Quit();
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
