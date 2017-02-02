using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelLogic : MonoBehaviour {

	private Vector2 startPosition;
	private Rigidbody2D rb;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		rb = GetComponent<Rigidbody2D>();
		startPosition = transform.position;
		StopRigidbodySimulation();
		GeneralLogic.StartSimulationEvent += StartSimulation;
		GeneralLogic.ResetSimulationEvent += ResetSimulation;
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		GeneralLogic.StartSimulationEvent -= StartSimulation;
		GeneralLogic.ResetSimulationEvent -= ResetSimulation;
	}

	public void StartSimulation()
	{
		rb.simulated = true;
	}

	public void ResetSimulation()
	{
		transform.position = startPosition;
		StopRigidbodySimulation();
		Debug.Log(rb.IsSleeping());
	}

	public void ResetSimulationInNewPosition()
	{
		StopRigidbodySimulation();
		startPosition = transform.position;
	}
	public void AddForce(Vector2 force)
	{
		rb.AddForce(force);
	}


	private void StopRigidbodySimulation()
	{
		rb.simulated = false;
		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0;
	}
}
