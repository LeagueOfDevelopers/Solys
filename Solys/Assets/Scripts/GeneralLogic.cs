using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralLogic : MonoBehaviour {

	public delegate void Action();
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
		ResetSimulation();
		OpenEndLevelMenu();
	}

	private void OpenEndLevelMenu()
	{
		///TODO
	}
}
