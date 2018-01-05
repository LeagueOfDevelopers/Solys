using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLogic : MonoBehaviour {

	public delegate void TargetAction();
	public static TargetAction TargetReached;

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" && TargetReached!=null)
		{
			TargetReached();
            other.gameObject.GetComponent<WheelLogic>().MoveToPortal(transform.position);
		}
	}
}
