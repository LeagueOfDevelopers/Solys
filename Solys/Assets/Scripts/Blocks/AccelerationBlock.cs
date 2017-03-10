using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationBlock : MonoBehaviour
{
    
    public float AccelStrength;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody)
        {

            other.gameObject.GetComponent<WheelLogic>().AddVelocity(AccelStrength);
        }

    }
}
