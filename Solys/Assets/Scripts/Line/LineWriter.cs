using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class LineWriter : MonoBehaviour
{
    private List<GameObject> ListLineRenderers; //It keeps all LineRenderers
    private List<Vector3> Positions; //It keeps all finger's positions, until u hold your finger on the screen.
    private List<Vector2> CollidersPositions; //It keeps all collider's positions, until u hold your finger on the screen.
    public GameObject LineRenderer; //Our line. It will create, when you touch the screen.
    private GameObject Wheel;
    public float DistanceBetweenDots; //Input number. 
    public float FrequencyPoints; //Input number.
    private bool isEnabled=true; //On/Off LineWriter
    private float distancePerDot;
    private List<Vector3> dotsForDrawing;
    private int LastPoint;
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
        distancePerDot = DistanceBetweenDots / FrequencyPoints;
        LastPoint = 3;
        dotsForDrawing = new List<Vector3>();
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
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnFingerSet(LeanFinger finger)
    {
        if (isEnabled)
        {
            if (Positions.Count == 0) // Когда только что поставили палец на экран
            {
                if (!finger.IsOverGui)
                {
                    GameObject lineRenderer = GameObject.Instantiate(LineRenderer);
                    lineRenderer.transform.parent = transform;
                    ListLineRenderers.Add(lineRenderer); //при каждом касании создавать новую линию.
                    Positions.Add(finger.GetWorldPosition(10, Camera.current)); //Самая первая точка касания
                    CollidersPositions.Clear();
                    CollidersPositions.Add(finger.GetWorldPosition(10, Camera.current));
                    // для коллайдера надо минимум две точки, поэтому создаем две!
                    CollidersPositions.Add(finger.GetWorldPosition(10, Camera.current));
                    ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<LineRenderer>().numPositions =
                        Positions.ToArray().Length;
                    ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<LineRenderer>()
                        .SetPositions(Positions.ToArray());
                    ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<EdgeCollider2D>().points =
                        CollidersPositions.ToArray();

                    LastPoint = 3;
                    dotsForDrawing = new List<Vector3>();
                }
            }
            else //Движение пальца на экране
            {
                if (!finger.IsOverGui)
                {
                    if (
                            Vector2.Distance(finger.GetWorldPosition(10, Camera.current),
                                CollidersPositions[CollidersPositions.Count - 1]) > DistanceBetweenDots)
                        //Проверка на минимальное расстояние между точками
                    {
                        var temporedFrequencyPoints = FrequencyPoints;
                        FrequencyPoints = Vector2.Distance(finger.GetWorldPosition(10, Camera.current),
                                              CollidersPositions[CollidersPositions.Count - 1]) - DistanceBetweenDots;
                        FrequencyPoints /= distancePerDot;
                        FrequencyPoints = (int) FrequencyPoints + temporedFrequencyPoints;
                        Positions.Add(finger.GetWorldPosition(10, Camera.current)); // Добавляем точку касания.
                        
                        //Это лист для хранения точек, которые мы будем рисовать на экране.

                        for (int i = LastPoint; i < (Positions.Count / 4) * 4; i += 3)
                            // Для каждых 4 точек мы используем формулу безье для нахождения дополнительных точек. (Positions.Count / 4) * 4 используется для того, чтобы убрать остаток. остаток прорабатывается в конце.
                        {
                            List<Vector3> drawingDotsInFourDots = GetAdditionalPoints(Positions[i - 3], Positions[i - 2],
                                Positions[i - 1], Positions[i]);
                            // Данный лист хранит 4 точки, а также дополнительные между ними.
                            for (int ii = 0; ii < drawingDotsInFourDots.Count - 1; ii++)
                                dotsForDrawing.Add(GetOffsetWheelDots(drawingDotsInFourDots[ii]));
                            // Из данного листа мы переносим точки в лист точек для отрисовки.
                            LastPoint = i;
                        }
                        LastPoint += 3;
                        if (Positions.Count % 4 != 0)
                            //Прорабатываем остаток.(если точек 6, то выше мы использовали формулу только для первых четырех точек, оставшиеся две имеют свою фомрулу безье)
                        {
                            if (Positions.Count % 4 == 1) dotsForDrawing.Add(GetOffsetWheelDots(Positions[Positions.Count - 1]));

                            if (Positions.Count % 4 == 2)
                            {
                                List<Vector3> drawingDotsInTwoDots = GetAdditionalPoints(Positions[Positions.Count - 2],
                                    Positions[Positions.Count - 1]);
                                for (int ii = 0; ii < drawingDotsInTwoDots.Count - 1; ii++)
                                    dotsForDrawing.Add(GetOffsetWheelDots(drawingDotsInTwoDots[ii]));
                            }

                            if (Positions.Count % 4 == 3)
                            {
                                List<Vector3> drawingDotsInThreeDots =
                                    GetAdditionalPoints(Positions[Positions.Count - 3],
                                        Positions[Positions.Count - 2], Positions[Positions.Count - 1]);
                                for (int ii = 0; ii < drawingDotsInThreeDots.Count - 1; ii++)
                                    dotsForDrawing.Add(GetOffsetWheelDots(drawingDotsInThreeDots[ii]));
                            }
                        }

                        CollidersPositions.Clear(); //Сброс листа точек для коллайдера
                        for (int i = 0; i < dotsForDrawing.Count; i++)
                            // Все точки для отрисовки мы добавляем в лист точек коллайдера
                            CollidersPositions.Add(dotsForDrawing[i]);

                        ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<LineRenderer>().numPositions =
                            dotsForDrawing.ToArray().Length;
                        ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<LineRenderer>()
                            .SetPositions(dotsForDrawing.ToArray());
                        ListLineRenderers[ListLineRenderers.Count - 1].GetComponent<EdgeCollider2D>().points =
                            CollidersPositions.ToArray();
                        FrequencyPoints = temporedFrequencyPoints;
                    }
                }
                else
                {
                    Positions = new List<Vector3>();
                    CollidersPositions = new List<Vector2>();
                }
            }
        }
    }

    public void OnFingerUp(LeanFinger finger)
    {
        float temporalDistance = DistanceBetweenDots; // Переменная для временного хранения дистанции между точек.
        DistanceBetweenDots = 0; //Чтобы формула была применима к последней точки касания, мы устанавливаем дистанцию временно равной 0.
        OnFingerSet(finger); //имитируем касание в точке отрыва пальца
        DistanceBetweenDots = temporalDistance; // Возвращаем значение дистанции между точек.
        Positions = new List<Vector3>();
        CollidersPositions = new List<Vector2>();
        LastPoint = 3;
        dotsForDrawing = new List<Vector3>();

    }
    public void StartSimulation()
    {
        Debug.Log("LineWriter Now Disabled");
        isEnabled = false;
        distancePerDot = DistanceBetweenDots / FrequencyPoints;
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

        Vector2 temp = new Vector2(p.x,p.y);
        temp = GetOffsetWheelDots(temp);


        return new Vector3(temp.x,temp.y,0);
    }
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)//Формула безье для 3 точек.
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; //first term

        p += 2 * u * t * p1; //second term
        p += tt * p2; //third term

        Vector2 temp = new Vector2(p.x,p.y);
        temp = GetOffsetWheelDots(temp);


        return new Vector3(temp.x,temp.y,0);
    }
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1)//Формула безье для 2 точек.
    {
        float u = 1 - t;

        Vector3 p = u * p0; //first term

        p += t * p1; //second term

        Vector2 temp = new Vector2(p.x,p.y);
        temp = GetOffsetWheelDots(temp);


        return new Vector3(temp.x,temp.y,0);
    }

    private Vector2 GetOffsetWheelDots(Vector2 dot)
    {
        Vector2 wheelPos = new Vector2(Wheel.transform.position.x,Wheel.transform.position.y);
        if(Vector2.Distance(dot, wheelPos)<Wheel.GetComponent<CircleCollider2D>().radius*Wheel.transform.localScale.x)
        {
            Vector2 direction = dot-wheelPos;
            direction = direction.normalized;
            dot = wheelPos+direction*(Wheel.GetComponent<CircleCollider2D>().radius+0.1f)*Wheel.transform.localScale.x;
            Debug.Log(direction);
        }
        return dot;
    }
}
