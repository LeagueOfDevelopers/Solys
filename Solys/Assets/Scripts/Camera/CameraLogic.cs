using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Lean.Touch;

public class CameraLogic : MonoBehaviour
{
    private bool isEnabled = false;
    public float CameraSpeed;
    private Vector3 LastPosition;
	void Start () {
	    LeanTouch.OnFingerDown += OnFingerDown;
	    LeanTouch.OnFingerUp += OnFingerUp;
        LeanTouch.OnFingerSet += OnFingerSet;
	}
    public void OnFingerSet(LeanFinger finger)
    {
       if (isEnabled)
        {
            if (LeanTouch.Fingers.Count == 1)
            {
                transform.position -=(finger.GetWorldPosition(10, Camera.current) - LastPosition);
                LastPosition = finger.GetWorldPosition(10, Camera.current);
            }


        }
    }
    public void OnFingerUp(LeanFinger finger)
    {
      

    }

    public void OnFingerDown(LeanFinger finger)
    {
        LastPosition = finger.GetWorldPosition(10,Camera.current);
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void setActive(bool state)
    {
        isEnabled = state;
    }
}
