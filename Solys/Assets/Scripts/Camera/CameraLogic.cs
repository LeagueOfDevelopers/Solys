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
    public float[] CameraSizes;
    private int state = 1;
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
                if (Mathf.Abs((Vector2.Distance(LeanTouch.Fingers[0].ScreenPosition,
                LeanTouch.Fingers[1].ScreenPosition) - LastDistance))>distanceForReSize)
                {
                    float difference = Vector2.Distance(LeanTouch.Fingers[0].ScreenPosition,
                LeanTouch.Fingers[1].ScreenPosition) - LastDistance;
                    if (difference > 0) CameraDown();
                    else CameraUp();

                    LastDistance = Vector2.Distance(LeanTouch.Fingers[0].ScreenPosition,
                LeanTouch.Fingers[1].ScreenPosition);
                }
            }


        }
    }
    public void CameraUp()
    {
         if (state<2)
            {
                state++;
                GetComponent<Camera>().orthographicSize = CameraSizes[state];
            }
    }
    public void CameraDown()
    {
        if (state > 0)
        {
            state--;
            GetComponent<Camera>().orthographicSize = CameraSizes[state];
            
        }
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
            LastDistance = Vector2.Distance(LeanTouch.Fingers[0].ScreenPosition, 
                LeanTouch.Fingers[1].ScreenPosition);
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
