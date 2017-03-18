using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Lean.Touch;

public class CameraLogic : MonoBehaviour
{
    private bool isEnabled = false;
    public float distanceForReSize;
    private float LastDistance;
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
            if (LeanTouch.Fingers.Count == 2)
            {
                if (Mathf.Abs((Vector2.Distance(LeanTouch.Fingers[0].GetWorldPosition(10, Camera.current), 
                    LeanTouch.Fingers[1].GetWorldPosition(10, Camera.current)) - LastDistance))>distanceForReSize)
                {
                    float difference = Vector2.Distance(LeanTouch.Fingers[0].GetWorldPosition(10, Camera.current),
                    LeanTouch.Fingers[1].GetWorldPosition(10, Camera.current)) - LastDistance;
                    if (difference > 0) CameraUp();
                    else CameraDown();

                    LastDistance = Vector2.Distance(LeanTouch.Fingers[0].GetWorldPosition(10, Camera.current),
                        LeanTouch.Fingers[1].GetWorldPosition(10, Camera.current));
                }
            }


        }
    }
    public void CameraUp()
    {

    }
    public void CameraDown()
    {

    }
    public void OnFingerUp(LeanFinger finger)
    {
      

    }

    public void OnFingerDown(LeanFinger finger)
    {
        if (LeanTouch.Fingers.Count == 1)
        {
        LastPosition = finger.GetWorldPosition(10,Camera.current);
        }
        if (LeanTouch.Fingers.Count == 2)
        {
            LastDistance = Vector2.Distance(LeanTouch.Fingers[0].GetWorldPosition(10, Camera.current), 
                LeanTouch.Fingers[1].GetWorldPosition(10,Camera.current));
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void setActive(bool state)
    {
        isEnabled = state;
    }
}
