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
		rb.Sleep();
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
		rb.WakeUp();
	}

	public void ResetSimulation()
	{
		rb.Sleep();
		transform.position = startPosition;
	}

	public void ResetSimulationInNewPosition()
	{
		rb.Sleep();
		startPosition = transform.position;
	}
	public void AddForce(Vector2 force)
	{
		rb.AddForce(force);
	}


}
