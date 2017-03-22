using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Lean.Touch;

public class CameraLogic : MonoBehaviour
{
    private Coroutine sizingCoroutine;
    private float currentSize;
    private float endSize;
    private float beginSize;
    public float timeForSize; //1 coroutine iteration
    private bool isEnabled = false;
    private bool isCanSize = true;
    public float distanceForReSize;
    private float LastDistance;
    private Vector3 LastPosition;
    public float[] CameraSizes;
    private int state = 1;
    void Start() {
        LeanTouch.OnFingerDown += OnFingerDown;
        LeanTouch.OnFingerUp += OnFingerUp;
        LeanTouch.OnFingerSet += OnFingerSet;
        currentSize = CameraSizes[state];
    }
    public void OnFingerSet(LeanFinger finger)
    {
        if (isEnabled)
        {
            if (LeanTouch.Fingers.Count == 1)
            {
                transform.position -= (finger.GetWorldPosition(10, Camera.current) - LastPosition);
                LastPosition = finger.GetWorldPosition(10, Camera.current);
            }
            if (LeanTouch.Fingers.Count == 2)
            {
                if (isCanSize)
                {
                    if (Mathf.Abs((Vector2.Distance(LeanTouch.Fingers[0].ScreenPosition,
                    LeanTouch.Fingers[1].ScreenPosition) - LastDistance)) > distanceForReSize)
                    {
                        float difference = Vector2.Distance(LeanTouch.Fingers[0].ScreenPosition,
                    LeanTouch.Fingers[1].ScreenPosition) - LastDistance;
                        if (difference > 0)
                        {
                            isCanSize = false;
                            CameraDown();
                        }
                        else
                        {
                            isCanSize = false;
                            CameraUp();
                        }

                        LastDistance = Vector2.Distance(LeanTouch.Fingers[0].ScreenPosition,
                    LeanTouch.Fingers[1].ScreenPosition);
                    }
                }
            }


        }
    }
    public void CameraUp()
    {
        if (state < 2)
        {
            state++;
            if (sizingCoroutine != null)
            {
                StopCoroutine(sizingCoroutine);
            }

            sizingCoroutine = StartCoroutine(CameraSizing());
        }
    }
    public void CameraDown()
    {
        if (state > 0)
        {
            state--;
            if (sizingCoroutine != null)
            {
                StopCoroutine(sizingCoroutine);
            }
            
            sizingCoroutine=StartCoroutine(CameraSizing());

        }
    }
    public void OnFingerUp(LeanFinger finger)
    {
        if (isEnabled)
        {
            if (LeanTouch.Fingers.Count == 2)
            {
                if (LeanTouch.Fingers[0].Equals(finger))
                {
                    LastPosition = LeanTouch.Fingers[1].GetWorldPosition(10, Camera.current);
                }
                else
                {
                    LastPosition = LeanTouch.Fingers[0].GetWorldPosition(10, Camera.current);
                }
                
            }
            isCanSize = true;
        }
    }
    IEnumerator CameraSizing()
    {
        currentSize=GetComponent<Camera>().orthographicSize;
        beginSize = currentSize;
        endSize = CameraSizes[state];
        for (int i = 1; i <= 100; i++)
        {
            currentSize = Mathf.Lerp(beginSize, endSize, (float)i/100.0f);
            GetComponent<Camera>().orthographicSize = currentSize;
            yield return new WaitForSeconds(timeForSize);
        }
    }

    public void OnFingerDown(LeanFinger finger)
    {
        if (isEnabled)
        {
            if (LeanTouch.Fingers.Count == 1)
            {
                LastPosition = finger.GetWorldPosition(10, Camera.current);
            }
            if (LeanTouch.Fingers.Count == 2)
            {
                LastDistance = Vector2.Distance(LeanTouch.Fingers[0].ScreenPosition,
                    LeanTouch.Fingers[1].ScreenPosition);
            }
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
