﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelBlock : MonoBehaviour
{
    public float AccelForce;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody)
        {
            Vector3 rotationInEuler = transform.rotation.eulerAngles;
            var x = Mathf.Cos(rotationInEuler.z*Mathf.PI/180);
            var y = Mathf.Sin(rotationInEuler.z * Mathf.PI / 180);
            other.GetComponent<WheelLogic>().AddForce(new Vector3(x,y) *AccelForce);
        }

    }
}
