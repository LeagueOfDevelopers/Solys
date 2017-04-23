using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Lean.Touch;

public class CameraLogic : MonoBehaviour
{
    private Vector2 MapSize;
    public GameObject LineWriter;
    public float timeForReturn;
    public float distanceForFollow;
    private int gameState = 0;
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
    private GameObject wheel;
    private Vector3 currentpos;
    private Vector3 endPos;
    private Vector3 beginPos;

    private void OnEnable()
    {
        GeneralLogic.StartSimulationEvent += StartSimulation;
        GeneralLogic.ResetSimulationEvent += ResetSimulation;
        GeneralLogic.StopSimulationEvent += StopSimulation;
    }
    private void OnDisable()
    {
        GeneralLogic.StartSimulationEvent -= StartSimulation;
        GeneralLogic.ResetSimulationEvent -= ResetSimulation;
        GeneralLogic.StopSimulationEvent -= StopSimulation;
    }
    void Start()
    {
        MapSize = GameObject.Find("Map").GetComponent<MapSerializator>().MapSize;
        wheel = GameObject.Find("Wheel(Clone)");
        LeanTouch.OnFingerDown += OnFingerDown;
        LeanTouch.OnFingerUp += OnFingerUp;
        LeanTouch.OnFingerSet += OnFingerSet;
        currentSize = CameraSizes[state];
    }
    public void OnFingerSet(LeanFinger finger)
    {


        if (LeanTouch.Fingers.Count == 1)
        {
            if (isEnabled)
            {
                if (gameState == 0)
                {
                    Vector3 FuturePos = transform.position - (finger.GetWorldPosition(10, Camera.current) - LastPosition);
                    if (FuturePos.x < 0 || FuturePos.x > MapSize.x * 15.0f)
                        FuturePos.x = 0;
                    if (FuturePos.y < 0 || FuturePos.y > MapSize.y * 10.5f)
                        FuturePos.y = 0;
                    transform.position = FuturePos;
                    

                }
            }
            LastPosition = finger.GetWorldPosition(10, Camera.current);
        }
        if (LeanTouch.Fingers.Count == 2)
        {
            if (gameState == 1 || (gameState == 0 && LineWriter.GetComponent<LineWriter>().tool == 2))
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

            sizingCoroutine = StartCoroutine(CameraSizing());

        }
    }
    public void OnFingerUp(LeanFinger finger)
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
    IEnumerator CameraSizing()
    {
        currentSize = GetComponent<Camera>().orthographicSize;
        beginSize = currentSize;
        endSize = CameraSizes[state];
        for (int i = 1; i <= 100; i++)
        {
            currentSize = Mathf.Lerp(beginSize, endSize, (float)i / 100.0f);
            GetComponent<Camera>().orthographicSize = currentSize;
            yield return new WaitForSeconds(timeForSize);
        }
    }

    public void OnFingerDown(LeanFinger finger)
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

    // Update is called once per frame
    void Update()
    {
        if (gameState == 1)
        {
            if (Mathf.Abs(Vector2.Distance(transform.position, wheel.transform.position))>distanceForFollow)
            {
                transform.Translate((wheel.transform.position - transform.position) * Time.deltaTime);
                Vector3 Height = transform.position;
                Height.z = -10;
                if (Height.x < 0 || Height.x > MapSize.x * 15.0f)
                    Height.x = 0;
                if (Height.y < 0 || Height.y > MapSize.y * 10.5f)
                    Height.y = 0;
                transform.position = Height;
            }
        }
    }
    public void setActive(bool state)
    {
        isEnabled = state;
    }
    public void StartSimulation()
    {
        gameState = 1;
        setActive(false);
    }

    public void ResetSimulation()
    {
        if (LineWriter.GetComponent<LineWriter>().tool == 2)
            setActive(true);
        gameState = 0;
        StartCoroutine("ReturnToWheelAfterSimulation");
    }
    public void StopSimulation()
    {
        if (LineWriter.GetComponent<LineWriter>().tool == 2)
            setActive(true);
        gameState = 0;
        StartCoroutine("ReturnToWheelAfterSimulation");
    }
    IEnumerator ReturnToWheelAfterSimulation()
    {
        yield return new WaitForEndOfFrame();
        currentpos = transform.position;
        beginPos = currentpos;
        endPos = wheel.transform.position;
        for (int i = 1; i <= 100; i++)
        {
            currentpos = Vector3.Lerp(beginPos, endPos, (float)i / 100.0f);
            currentpos = new Vector3(currentpos.x, currentpos.y, -10);
            if (currentpos.x < 0 || currentpos.x > MapSize.x * 15.0f)
                currentpos.x = 0;
            if (currentpos.y < 0 || currentpos.y > MapSize.y * 10.5f)
                currentpos.y = 0;
            transform.position = currentpos;
            yield return new WaitForSeconds(timeForReturn);
        }
    }
}