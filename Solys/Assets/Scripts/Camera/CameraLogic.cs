using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Lean.Touch;

public class CameraLogic : MonoBehaviour
{
    private float lifeBetweenLineWriterAndSwipe;
    public float CameraSpeed;
    private bool isTouchScreen;
    private bool isTouchScreenForKeyDown;
    public float distanceForSwipe;
    private Vector2[] fingersPositions;
	// Use this for initialization
	void Start () {
		fingersPositions = new Vector2[4];
	    LeanTouch.OnFingerDown += OnFingerDown;
	    LeanTouch.OnFingerUp += OnFingerUp;
        LeanTouch.OnFingerSet += OnFingerSet;
        lifeBetweenLineWriterAndSwipe = GameObject.Find("LineWriter").GetComponent<LineWriter>().lifeBetweenLineWriterAndSwipe;
	}
    private float accel = 0;
    public void OnFingerSet(LeanFinger finger)
    {
        if ((LeanTouch.Fingers.Count == 2 && isTouchScreen))
        {
            if ((Mathf.Abs(LeanTouch.Fingers[0].Age - LeanTouch.Fingers[1].Age) <= lifeBetweenLineWriterAndSwipe))
            {
                fingersPositions[finger.Index + 2] = finger.ScreenPosition;
                if (LeanTouch.Fingers.Count == 2)
                {
                    int ii = 0;
                    if (finger.Index == 0) ii = 1;
                    else ii = 0;
                    fingersPositions[ii + 2] = finger.ScreenPosition;
                }
                if (Vector2.Distance(fingersPositions[1], fingersPositions[3]) > distanceForSwipe &&
                    Vector2.Distance(fingersPositions[2], fingersPositions[0]) > distanceForSwipe)
                {
                    Vector2 AveragePosStart = (fingersPositions[0] + fingersPositions[1]) / 2;
                    Vector2 AveragePosEnd = (fingersPositions[2] + fingersPositions[3]) / 2;

                    if (Vector2.Distance(AveragePosEnd, AveragePosStart) > distanceForSwipe)
                    {
                        accel = Vector2.Distance(AveragePosEnd, AveragePosStart) / 100;
                        Vector2 direction = AveragePosEnd - AveragePosStart;
                        direction = direction.normalized;
                        //   rb.AddForce(direction * CameraSpeed);
                        //Lerp 
                        transform.position += (Vector3)direction * CameraSpeed * accel;
                    }
                }
            }
            

        }
    }
    public void OnFingerUp(LeanFinger finger)
    {
        fingersPositions = new Vector2[4];
        isTouchScreen = false;

    }

    public void OnFingerDown(LeanFinger finger)
    {
        if (LeanTouch.Fingers.Count == 1)
        {
            fingersPositions[0] = finger.ScreenPosition;
            isTouchScreenForKeyDown = true;
        }
        if (LeanTouch.Fingers.Count == 2)
        {
            isTouchScreen = true;
            if (!isTouchScreenForKeyDown)
                fingersPositions[0] = finger.ScreenPosition;
            isTouchScreenForKeyDown = false;
            fingersPositions[1] = finger.ScreenPosition;
        }
    }

    // Update is called once per frame
        void Update () {
		
	}
}
