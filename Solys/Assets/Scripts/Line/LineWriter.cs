using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Lean.Touch;
using UnityEngine.UI;

public class LineWriter : MonoBehaviour
{
    private float temporedFrequencyPoints;
    private GameObject quanityUI;
    private float lineCost = 0.01f;
    public float startQuanityLines;
    private float quanityLines;
    private List<GameObject> ListLineRenderers; //It keeps all LineRenderers
    private List<Vector3> Positions; //It keeps all finger's positions, until u hold your finger on the screen.
    private List<Vector2> CollidersPositions; //It keeps all collider's positions, until u hold your finger on the screen.
    public GameObject LineRenderer; //Our line. It will create, when you touch the screen.
    private GameObject Wheel;
    public float DistanceBetweenDots; //Input number. 
    public float FrequencyPoints; //Input number.
    private bool isEnabled = true; //On/Off LineWriter
    private float distancePerDot;
    private List<Vector3> dotsForDrawing;
    private int LastPoint;
    private int MainFinger;
    public int tool; // 0 - Рисовалка линий, 1 - ластик
    public float EraseSize;
    public float DistanceForContinueLine;
    public float LineRemainder { get { return quanityLines / startQuanityLines; } }
    private int lastDots;
    private bool isOnCooldownAfterChangeTool = false;

    public int PositionsAmount
    {
        get
        {
            return Positions.Count;
        }
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        
        Wheel = GameObject.Find("Wheel");
        Positions = new List<Vector3>();
        CollidersPositions = new List<Vector2>();
        ListLineRenderers = new List<GameObject>();
        LeanTouch.OnFingerSet += OnFingerSet;
        LeanTouch.OnFingerUp += OnFingerUp;
        GeneralLogic.StartSimulationEvent += StartSimulation;
        GeneralLogic.ResetSimulationEvent += ResetSimulation;
        GeneralLogic.StopSimulationEvent += StopSimulation;
        GeneralLogic.PauseBlockActivatedEvent += PauseBlockActivated;
        distancePerDot = DistanceBetweenDots / FrequencyPoints;
        LastPoint = 3;
        lastDots = 0;
        MainFinger = -1;
        dotsForDrawing = new List<Vector3>();
        quanityUI = GameObject.Find("Slider");

    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        LeanTouch.OnFingerSet -= OnFingerSet;
        LeanTouch.OnFingerUp -= OnFingerUp;
        GeneralLogic.StartSimulationEvent -= StartSimulation;
        GeneralLogic.ResetSimulationEvent -= ResetSimulation;
        GeneralLogic.StopSimulationEvent -= StopSimulation;
        GeneralLogic.PauseBlockActivatedEvent -= PauseBlockActivated;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Start()
    {
        StartCoroutine(SmoothQuantityChange());
        lastDots = 0;
        quanityLines = startQuanityLines;
    }
    public string FoundLinesEndsNear(ref float minDistance, Vector2 PosNow,bool isFingerUpAction)
    {
        string FoundDescription = "Not Found";
        int maxIter = ListLineRenderers.Count;
        maxIter = isFingerUpAction ? maxIter - 1 : maxIter;
        for (int i = 0; i < maxIter; i++)
        {
            Vector2[] ExpectLine = ListLineRenderers[i].GetComponent<EdgeCollider2D>().points;
            if (Vector2.Distance(PosNow, ExpectLine[ExpectLine.Length - 1]) <= minDistance)
            {
                minDistance = Vector2.Distance(PosNow, ExpectLine[ExpectLine.Length - 1]);
                FoundDescription = i.ToString() + 'E';
            }
            if (Vector2.Distance(PosNow, ExpectLine[0]) <= minDistance)
            {
                minDistance = Vector2.Distance(PosNow, ExpectLine[0]);
                FoundDescription = i.ToString() + 'S';
            }
        }
        return FoundDescription;
    }
    public Vector2[] DeleteAndTakeLine(string FoundDescription, bool isFingerUpAction)
    {
        char CharPos = FoundDescription[FoundDescription.Length - 1];
        int lineNumber = int.Parse(FoundDescription.Substring(0, FoundDescription.Length - 1));
        Vector2[] ExpectLine =
            ListLineRenderers[lineNumber]
                .GetComponent<EdgeCollider2D>().points;

        Destroy(ListLineRenderers[lineNumber]);
        ListLineRenderers.RemoveAt(lineNumber);
        if ((CharPos == 'S' && !isFingerUpAction) || ((isFingerUpAction) && CharPos == 'E'))
            ExpectLine = ReverseArray(ExpectLine);
        
        return ExpectLine;
    }
    public void ContinueAnotherLine(LeanFinger finger, string FoundDescription)
    {
        Vector2[] ExpectLine = DeleteAndTakeLine(FoundDescription, false);

        GameObject lineRenderer = GameObject.Instantiate(LineRenderer);
        lineRenderer.transform.parent = transform;
        ListLineRenderers.Add(lineRenderer);

        dotsForDrawing = new List<Vector3>();
        for (int i = 0; i < ExpectLine.Length; i++)
        {
            Positions.Add(ExpectLine[i]);
            CollidersPositions.Add(ExpectLine[i]);
            dotsForDrawing.Add(ExpectLine[i]);
        }

        ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<LineRenderer>().positionCount =
            Positions.ToArray().Length;
        ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<LineRenderer>()
            .SetPositions(Positions.ToArray());
        ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<EdgeCollider2D>().points =
            CollidersPositions.ToArray();

        MainFinger = finger.Index;
        LastPoint = (Positions.Count / 4) * 4;
    }
    public void StartNewLine(LeanFinger finger)
    {
        if (quanityLines > 0)
        {
            GameObject lineRenderer = GameObject.Instantiate(LineRenderer);
            lineRenderer.transform.parent = transform;
            ListLineRenderers.Add(lineRenderer); //при каждом касании создавать новую линию.
            Positions.Add(finger.GetWorldPosition(10, Camera.current)); //Самая первая точка касания
            CollidersPositions.Clear();
            CollidersPositions.Add(finger.GetWorldPosition(10, Camera.current));
            // для коллайдера надо минимум две точки, поэтому создаем две!
            CollidersPositions.Add(finger.GetWorldPosition(10, Camera.current));
            ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<LineRenderer>().positionCount
                =
                Positions.ToArray().Length;
            ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<LineRenderer>()
                .SetPositions(Positions.ToArray());
            ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<EdgeCollider2D>().points =
                CollidersPositions.ToArray();
            UpdateQuanityLines();
            MainFinger = finger.Index;
            LastPoint = 3;
            dotsForDrawing = new List<Vector3>();
        }
    }
    public void RecalculateFrequencyPoints(LeanFinger finger)
    {
        temporedFrequencyPoints = FrequencyPoints;
        FrequencyPoints = Vector2.Distance(finger.GetWorldPosition(10, Camera.current),
                              CollidersPositions[CollidersPositions.Count - 1]) - DistanceBetweenDots;
        FrequencyPoints /= distancePerDot;
        FrequencyPoints = (int)FrequencyPoints + temporedFrequencyPoints;
    }
    public void CalculateRemainder(LeanFinger finger)
    {
        if (Positions.Count % 4 == 1)
        {
            lastDots = 1;
            dotsForDrawing.Add(GetOffsetWheelDots(Positions[Positions.Count - 1]));
        }

        if (Positions.Count % 4 == 2)
        {
            List<Vector3> drawingDotsInTwoDots =
                GetAdditionalPoints(Positions[Positions.Count - 2],
                    Positions[Positions.Count - 1]);
            for (int ii = 0; ii < drawingDotsInTwoDots.Count; ii++)
                dotsForDrawing.Add(GetOffsetWheelDots(drawingDotsInTwoDots[ii]));
            lastDots = drawingDotsInTwoDots.Count;
        }

        if (Positions.Count % 4 == 3)
        {
            List<Vector3> drawingDotsInThreeDots =
                GetAdditionalPoints(Positions[Positions.Count - 3],
                    Positions[Positions.Count - 2], Positions[Positions.Count - 1]);
            for (int ii = 0; ii < drawingDotsInThreeDots.Count; ii++)
                dotsForDrawing.Add(GetOffsetWheelDots(drawingDotsInThreeDots[ii]));
            lastDots = drawingDotsInThreeDots.Count;
        }
    }
    public void CalculateMainLine(LeanFinger finger)
    {
        if(lastDots>0)
        {
            if (dotsForDrawing.Count > lastDots)
                dotsForDrawing.RemoveRange(dotsForDrawing.Count - lastDots, lastDots);
            else
                dotsForDrawing.Clear();
            lastDots = 0;
        }
        for (int i = LastPoint; i < (Positions.Count / 4) * 4; i += 3)
        // Для каждых 4 точек мы используем формулу безье для нахождения дополнительных точек. (Positions.Count / 4) * 4 используется для того, чтобы убрать остаток. остаток прорабатывается в конце.
        {
            List<Vector3> drawingDotsInFourDots = GetAdditionalPoints(Positions[i - 3],
                Positions[i - 2], Positions[i - 1], Positions[i]);
            // Данный лист хранит 4 точки, а также дополнительные между ними.
            for (int ii = 0; ii < drawingDotsInFourDots.Count - 1; ii++)
                dotsForDrawing.Add(GetOffsetWheelDots(drawingDotsInFourDots[ii]));
            // Из данного листа мы переносим точки в лист точек для отрисовки.
            LastPoint = i+3;
        }
    }
    public void DrawContinueLine(LeanFinger finger)
    {
        if (Vector2.Distance(finger.GetWorldPosition(10, Camera.current),
                CollidersPositions[CollidersPositions.Count - 1]) > DistanceBetweenDots)
        //Проверка на минимальное расстояние между точками
        {
            RecalculateFrequencyPoints(finger);
            Positions.Add(finger.GetWorldPosition(10, Camera.current)); // Добавляем точку касания.
            CalculateMainLine(finger);
            if (Positions.Count % 4 != 0)
            //Прорабатываем остаток.(если точек 6, то выше мы использовали формулу только для первых четырех точек, оставшиеся две имеют свою фомрулу безье)
            {
                CalculateRemainder(finger);
            }

            UpdateQuanityLines();
            CollidersPositions.Clear(); //Сброс листа точек для коллайдера
            for (int i = 0; i < dotsForDrawing.Count; i++)
                // Все точки для отрисовки мы добавляем в лист точек коллайдера
                CollidersPositions.Add(dotsForDrawing[i]);

            ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<LineRenderer>().positionCount
                = dotsForDrawing.ToArray().Length;
            ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<LineRenderer>()
                .SetPositions(dotsForDrawing.ToArray());
            ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<EdgeCollider2D>().points =
                CollidersPositions.ToArray();
            FrequencyPoints = temporedFrequencyPoints;
        }
    }
    public void OnFingerSetWriter(LeanFinger finger)
    {
        if (isEnabled)
        {
            if (Positions.Count == 0) // Когда только что поставили палец на экран
            {
                if (MainFinger == -1)
                    if (!finger.IsOverGui && !isOverDragableObject(finger))
                    {
                        Vector2 PosNow = finger.GetWorldPosition(10, Camera.current);
                        float minDistance = DistanceBetweenDots + 1;
                        string FoundDescription = FoundLinesEndsNear(ref minDistance, PosNow,false);
                        if (minDistance <= DistanceForContinueLine)
                        {
                            ContinueAnotherLine(finger, FoundDescription);
                        }
                        else
                        {
                            StartNewLine(finger);
                        }
                    }
            }
            else //Движение пальца на экране
            {
                if (finger.Index == MainFinger)
                    if (quanityLines > 0)
                    {
                        if (!finger.IsOverGui)
                        {
                            DrawContinueLine(finger);
                        }
                        else
                        {
                            Positions = new List<Vector3>();
                            MainFinger = -1;
                            LastPoint = 3;
                            lastDots = 0;
                            CollidersPositions = new List<Vector2>();
                        }
                    }
            }
        }
    }
    public void OnFingerSet(LeanFinger finger)
    {
        if (isOnCooldownAfterChangeTool) return;

        switch (tool)
        {
            case 0:
                OnFingerSetWriter(finger);
                break;
            case 1:
                OnFingerSetEraser(finger);
                break;
        }
    }
    public void GetStartAndEndIndexs(ref int indexForStartErase, ref int indexForEndErase, Vector2[] Expectline, Vector2 PosNow)
    {
        int j = 0;
        while (j < Expectline.Length)
        {
            if (Vector2.Distance(PosNow, Expectline[j]) <= EraseSize * Camera.main.GetComponent<CameraLogic>().GetZoomMultiplyer)
            {
                if (indexForStartErase == -1) indexForStartErase = j;
                else indexForEndErase = j;
            }
            else if (indexForEndErase != -1) break;
            j++;

        }
    }
    public void OnFingerSetEraser(LeanFinger finger)
    {
        if (isEnabled)
        {
            Vector2 PosNow = finger.GetWorldPosition(10, Camera.current);
            for (int i = 0; i < ListLineRenderers.Count; i++)
            {
                Vector2[] Expectline = ListLineRenderers[i].GetComponent<EdgeCollider2D>().points;
                int indexForStartErase = -1;
                int indexForEndErase = -1;
                GetStartAndEndIndexs(ref indexForStartErase, ref indexForEndErase, Expectline, PosNow);
                if (indexForStartErase == -1) continue;
                if (indexForEndErase != -1)
                {
                    List<Vector3> FirstArrayForLine = new List<Vector3>();
                    List<Vector2> FirstArrayForLineForCollider = new List<Vector2>();
                    List<Vector3> SecondArrayForLine = new List<Vector3>();
                    List<Vector2> SecondArrayForLineForCollider = new List<Vector2>();


                    for (int ii = 0; ii < indexForStartErase; ii++)
                    {
                        FirstArrayForLine.Add(Expectline[ii]);
                        FirstArrayForLineForCollider.Add(Expectline[ii]);
                    }
                    for (int ii = indexForEndErase + 1; ii < Expectline.Length; ii++)
                    {
                        SecondArrayForLine.Add(Expectline[ii]);
                        SecondArrayForLineForCollider.Add(Expectline[ii]);
                    }
                    UpdateQuanityLines();

                    Destroy(ListLineRenderers[i]);
                    ListLineRenderers.RemoveAt(i);

                    GameObject lineRenderer;
                    if (FirstArrayForLine.Count >= 2 || SecondArrayForLine.Count >= 2)
                    {
                        if (FirstArrayForLine.Count >= 2)
                        {
                            lineRenderer = GameObject.Instantiate(LineRenderer);
                            lineRenderer.transform.parent = transform;
                            lineRenderer.GetComponent<LineRenderer>().positionCount =
                                FirstArrayForLine.ToArray().Length;
                            lineRenderer.GetComponent<LineRenderer>()
                                .SetPositions(FirstArrayForLine.ToArray());
                            lineRenderer.GetComponent<EdgeCollider2D>().points =
                                FirstArrayForLineForCollider.ToArray();
                            ListLineRenderers.Add(lineRenderer);
                        }
                        if (SecondArrayForLine.Count >= 2)
                        {
                            lineRenderer = GameObject.Instantiate(LineRenderer);
                            lineRenderer.transform.parent = transform;
                            lineRenderer.GetComponent<LineRenderer>().positionCount =
                                SecondArrayForLine.ToArray().Length;
                            lineRenderer.GetComponent<LineRenderer>()
                                .SetPositions(SecondArrayForLine.ToArray());
                            lineRenderer.GetComponent<EdgeCollider2D>().points =
                                SecondArrayForLineForCollider.ToArray();
                            ListLineRenderers.Add(lineRenderer);
                        }
                        break;
                    }

                }
            }
        }

    }
    private bool isOverDragableObject(LeanFinger finger)
    {
        Vector3 origin = Camera.main.ScreenToWorldPoint(finger.ScreenPosition);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero);
        if (hit)
        {
            var element = hit.transform.gameObject.GetComponent<MapElement>();
            if (element != null)
            {
                if (element.isDragable)
                    return true;
            }
        }
        return false;
    }

    public Vector2[] ReverseArray(Vector2[] ExpectLine)
    {
        Vector2[] ForReturn = new Vector2[ExpectLine.Length];

        for (int i = 0; i < ExpectLine.Length; i++)
        {
            ForReturn[ExpectLine.Length - i - 1] = ExpectLine[i];
        }

        return ForReturn; 
    }

    public void OnFingerUp(LeanFinger finger)
    {
        if (isOnCooldownAfterChangeTool) return;

        if (finger.Index == MainFinger)
        {
            float temporalDistance = DistanceBetweenDots; // Переменная для временного хранения дистанции между точек.
            DistanceBetweenDots = 0;
            //Чтобы формула была применима к последней точки касания, мы устанавливаем дистанцию временно равной 0.
            OnFingerSet(finger); //имитируем касание в точке отрыва пальца
            DistanceBetweenDots = temporalDistance; // Возвращаем значение дистанции между точек.

            Vector2 PosNow = finger.GetWorldPosition(10, Camera.current);
            float minDistance = DistanceBetweenDots + 1;
            string FoundDescription = FoundLinesEndsNear(ref minDistance, PosNow, true);
            if (minDistance <= DistanceForContinueLine)
            {
                Vector2[] ExpectLine = DeleteAndTakeLine(FoundDescription,true);
                for (int i = 0; i < ExpectLine.Length; i++)
                {
                    Positions.Add(ExpectLine[i]);
                    CollidersPositions.Add(ExpectLine[i]);

                }
                ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<LineRenderer>().positionCount =
                    Positions.ToArray().Length;
                ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<LineRenderer>()
                    .SetPositions(Positions.ToArray());
                ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<EdgeCollider2D>().points =
                    CollidersPositions.ToArray();

                MainFinger = finger.Index;
                LastPoint = 3;
                lastDots = 0;
                dotsForDrawing = new List<Vector3>();
            }


            Positions = new List<Vector3>();
            CollidersPositions = new List<Vector2>();
            LastPoint = 3;
            lastDots = 0;
            MainFinger = -1;
            dotsForDrawing = new List<Vector3>();
        }

    }
    public void StartSimulation()
    {
        Debug.Log("LineWriter Now Disabled");
        isEnabled = false;
        distancePerDot = DistanceBetweenDots / FrequencyPoints;
    }

    public void StopSimulation()
    {
        isEnabled = true;
    }

    public void PauseBlockActivated()
    {
        isEnabled = true;
        for (int i = 0; i < ListLineRenderers.Count; i++)
        {
            Destroy(ListLineRenderers[i]);
        }
        ListLineRenderers.Clear();
    }

    public void ResetSimulation()
    {
        Debug.Log("LineWriter Was Reset");
        for (int i = 0; i < ListLineRenderers.Count; i++)
        {
            Destroy(ListLineRenderers[i]);
        }
        ListLineRenderers.Clear();
        Debug.Log("LineWriter Now Enabled");
        isEnabled = true;
        quanityLines = startQuanityLines;
        
    }

    private List<Vector3> GetAdditionalPoints(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) //Метод для получения дополнительных точек среди 4 точек.
    {
        List<Vector3> dotsForDrawing = new List<Vector3>();
        float t = 0.0f;
        while (t < 1.0f)
        {
            dotsForDrawing.Add(CalculateBezierPoint(t, p0, p1, p2, p3));
            t += 1.0f / FrequencyPoints; //FrequencyPoints показывает кол-во дополнительных точек.

        }
        dotsForDrawing.Add(CalculateBezierPoint(1.0f, p0, p1, p2, p3));



        return dotsForDrawing;
    }
    private List<Vector3> GetAdditionalPoints(Vector3 p0, Vector3 p1, Vector3 p2)//Метод для получения дополнительных точек среди 3 точек.
    {
        List<Vector3> dotsForDrawing = new List<Vector3>();
        float t = 0.0f;
        while (t < 1.0f)
        {
            dotsForDrawing.Add(CalculateBezierPoint(t, p0, p1, p2));
            t += 1.0f / FrequencyPoints;

        }
        dotsForDrawing.Add(CalculateBezierPoint(1.0f, p0, p1, p2));


        return dotsForDrawing;
    }
    private List<Vector3> GetAdditionalPoints(Vector3 p0, Vector3 p1)//Метод для получения дополнительных точек среди 2 точек.
    {
        List<Vector3> dotsForDrawing = new List<Vector3>();
        float t = 0.0f;
        while (t < 1.0f)
        {
            dotsForDrawing.Add(CalculateBezierPoint(t, p0, p1));
            t += 1.0f / FrequencyPoints;

        }
        dotsForDrawing.Add(CalculateBezierPoint(1.0f, p0, p1));


        return dotsForDrawing;
    }
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)//Формула безье для 4 точек.
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0; //first term

        p += 3 * uu * t * p1; //second term
        p += 3 * u * tt * p2; //third term
        p += ttt * p3; //fourth term

        Vector2 temp = new Vector2(p.x, p.y);
        temp = GetOffsetWheelDots(temp);


        return new Vector3(temp.x, temp.y, 0);
    }
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)//Формула безье для 3 точек.
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; //first term

        p += 2 * u * t * p1; //second term
        p += tt * p2; //third term

        Vector2 temp = new Vector2(p.x, p.y);
        temp = GetOffsetWheelDots(temp);


        return new Vector3(temp.x, temp.y, 0);
    }
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1)//Формула безье для 2 точек.
    {
        float u = 1 - t;

        Vector3 p = u * p0; //first term

        p += t * p1; //second term

        Vector2 temp = new Vector2(p.x, p.y);
        temp = GetOffsetWheelDots(temp);


        return new Vector3(temp.x, temp.y, 0);
    }

    private Vector2 GetOffsetWheelDots(Vector2 dot)
    {
        Vector2 wheelPos = new Vector2(Wheel.transform.position.x, Wheel.transform.position.y);
        if (Vector2.Distance(dot, wheelPos) < Wheel.GetComponent<CircleCollider2D>().radius * Wheel.transform.localScale.x)
        {
            Vector2 direction = dot - wheelPos;
            direction = direction.normalized;
            dot = wheelPos + direction * (Wheel.GetComponent<CircleCollider2D>().radius + 0.1f) * Wheel.transform.localScale.x;
            Debug.Log(direction);
        }
        return dot;
    }

    public void ChangeTool(int nextTool)
    {
        if(nextTool == tool) return;
        switch (nextTool)
        {
            case 1:
                tool = 1;
                Camera.main.GetComponent<CameraLogic>().isActive = false;
                break;
            case 2:
                if (ListLineRenderers.Count > 0)
                {
                    GameObject renderer = ListLineRenderers[ListLineRenderers.Count - 1];
                    if (renderer.GetComponent<LineRenderer>().positionCount < 15)
                    {
                        ListLineRenderers.RemoveAt(ListLineRenderers.Count - 1);
                        Destroy(renderer);
                    }
                    Positions.Clear();
                }
                MainFinger = -1;
                tool = 2;
                Camera.main.GetComponent<CameraLogic>().isActive = true;
                break;
            case 0:
                tool = 0;
                Camera.main.GetComponent<CameraLogic>().isActive = false;
                break;

        }
    }

    public void SetCooldownOnChangeTool()
    {
        StartCoroutine(SetCooldownAfterChangeTool());
    }

    private IEnumerator SetCooldownAfterChangeTool()
    {
        isOnCooldownAfterChangeTool = true;
        yield return new WaitForSeconds(0.1f);
        isOnCooldownAfterChangeTool = false;
    }

    private void UpdateQuanityLines()
    {
        //(Mathf.Abs(dotsForDrawing.Count - CollidersPositions.Count)) * lineCost;
        if (ListLineRenderers.Count == 0)
        {
            quanityLines = startQuanityLines;
            //quanityUI.GetComponent<Slider>().value = quanityLines / startQuanityLines;
            return;
        }
        float tempQuantity = 0; ;
        foreach (GameObject line in ListLineRenderers)
        {
            int lenght = line.GetComponent<LineRenderer>().positionCount;
            tempQuantity += lenght * lineCost;
        }

        quanityLines = startQuanityLines - tempQuantity;
        //quanityUI.GetComponent<Slider>().value = quanityLines / startQuanityLines;
    }

    IEnumerator SmoothQuantityChange()
    {
        float current = quanityUI.GetComponent<Slider>().value;
        float target = quanityLines / startQuanityLines;
        quanityUI.GetComponent<Slider>().value = Mathf.Lerp(current, target, Time.deltaTime*10);
        yield return new WaitForEndOfFrame();
        StartCoroutine(SmoothQuantityChange());
    }
}