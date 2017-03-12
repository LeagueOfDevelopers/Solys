using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Lean.Touch;

public class CameraLogic : MonoBehaviour
{

    public float CameraSpeed;
    private bool isTouchScreen;
    private bool isTouchScreenForKeyDown;
    public Vector2 size;
    public Vector2 position;
    public float distanceForSwipe;
    private Vector2[] fingersPositions;
	// Use this for initialization
	void Start () {
		fingersPositions = new Vector2[4];
	    LeanTouch.OnFingerDown += OnFingerDown;
	    LeanTouch.OnFingerUp += OnFingerUp;
	}

    public void OnFingerUp(LeanFinger finger)
    {
       
        if (LeanTouch.Fingers.Count == 2)
        {
            fingersPositions[finger.Index+2] = finger.GetWorldPosition(10, Camera.current);
        }
        if (LeanTouch.Fingers.Count == 1 || (LeanTouch.Fingers.Count == 2 && isTouchScreen))
        {
            fingersPositions[finger.Index + 2] = finger.GetWorldPosition(10, Camera.current);
            if (LeanTouch.Fingers.Count == 2)
            {
                int ii = 0;
                if (finger.Index == 0) ii = 1;
                else ii = 0;
                fingersPositions[ii + 2] = finger.GetWorldPosition(10, Camera.current);
            }
            if (Vector2.Distance(fingersPositions[1], fingersPositions[3]) > distanceForSwipe &&
                Vector2.Distance(fingersPositions[2], fingersPositions[0]) > distanceForSwipe)
            {
                Vector2 AveragePosStart = (fingersPositions[0] + fingersPositions[1]) / 2;
                Vector2 AveragePosEnd = (fingersPositions[2] + fingersPositions[3]) / 2;

                if (Vector2.Distance(AveragePosEnd, AveragePosStart) > distanceForSwipe)
                {
                    Vector2 direction = AveragePosEnd-AveragePosStart;
                    direction = direction.normalized;
                 //   rb.AddForce(direction * CameraSpeed);
                   //Lerp 
                }
            }
            fingersPositions = new Vector2[4];
                isTouchScreen = false;
            
        }
    }

    public void OnFingerDown(LeanFinger finger)
    {
        if (LeanTouch.Fingers.Count == 1)
        {
            fingersPositions[0] = finger.GetWorldPosition(10, Camera.current);
            isTouchScreenForKeyDown = true;
        }
        if (LeanTouch.Fingers.Count == 2)
        {
            isTouchScreen = true;
            if (!isTouchScreenForKeyDown)
                fingersPositions[0] = finger.GetWorldPosition(10, Camera.current);
            isTouchScreenForKeyDown = false;
            fingersPositions[1] = finger.GetWorldPosition(10, Camera.current);
        }
    }

    // Update is called once per frame
        void Update () {
		
	}
}
