using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelLogic : MonoBehaviour {

	public Vector2 gravityForce;
	private Vector2 startPosition;
	private Rigidbody rb;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		rb = GetComponent<Rigidbody>();
		startPosition = transform.position;
		rb.useGravity = false;
		rb.Sleep();
	}

	public void StartSimulation()
	{
		rb.WakeUp();
		SetGravitationVector(gravityForce);
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
	public void SetGravitationVector(Vector2 force)
	{
		if(gravityForce!=null)
			rb.AddForce(-gravityForce);
		gravityForce = force;
		rb.AddForce(gravityForce);

	}


}
