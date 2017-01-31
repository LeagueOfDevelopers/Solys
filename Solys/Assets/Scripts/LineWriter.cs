using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class LineWriter : MonoBehaviour
{
    List<GameObject> ListLR;
    List<Vector3> pos;
    List<Vector2> posCol;
    public GameObject LineRenderer;
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        pos = new List<Vector3>();
        posCol = new List<Vector2>();
        ListLR = new List<GameObject>();
        LeanTouch.OnFingerSet += OnFingerSet;
        LeanTouch.OnFingerDown += OnFingerDown;
        LeanTouch.OnFingerUp += OnFingerUp;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        LeanTouch.OnFingerSet -= OnFingerSet;
        LeanTouch.OnFingerDown -= OnFingerDown;
        LeanTouch.OnFingerUp -= OnFingerUp;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnFingerSet(LeanFinger finger)
    {
        if (pos.Count == 0)
        {
            pos.Add(finger.GetWorldPosition(10, Camera.current));
            posCol.Clear();
            posCol.Add(finger.GetWorldPosition(10, Camera.current));
            posCol.Add(finger.GetWorldPosition(10, Camera.current));
            ListLR[ListLR.Count - 1].GetComponent<LineRenderer>().numPositions = pos.ToArray().Length;
            ListLR[ListLR.Count - 1].GetComponent<LineRenderer>().SetPositions(pos.ToArray());
            ListLR[ListLR.Count - 1].GetComponent<EdgeCollider2D>().points = posCol.ToArray();
        }
        else
        {
            if (Vector2.Distance(finger.ScreenPosition, posCol[posCol.Count - 1]) > 10)
            {
                pos.Add(finger.GetWorldPosition(10, Camera.current));

                List<Vector3> drawingDots = new List<Vector3>();

                for (int i = 3; i < (pos.Count / 4) * 4; i += 3)
                {
                    List<Vector3> drawingDotsInFourDots = GetDrawingDots(pos[i - 3], pos[i - 2], pos[i - 1], pos[i]);
                    for (int ii = 0; ii < drawingDotsInFourDots.Count - 1; ii++)
                        drawingDots.Add(drawingDotsInFourDots[ii]);
                }

                if (pos.Count % 4 != 0)
                {
                    if (pos.Count % 4 == 1) drawingDots.Add(pos[pos.Count - 1]);

                    if (pos.Count % 4 == 2)
                    {
                        List<Vector3> drawingDotsInFourDots = GetDrawingDots(pos[pos.Count - 2], pos[pos.Count - 1]);
                        for (int ii = 0; ii < drawingDotsInFourDots.Count - 1; ii++)
                            drawingDots.Add(drawingDotsInFourDots[ii]);
                    }

                    if (pos.Count % 4 == 3)
                    {
                        List<Vector3> drawingDotsInFourDots = GetDrawingDots(pos[pos.Count - 3], pos[pos.Count - 2], pos[pos.Count - 1]);
                        for (int ii = 0; ii < drawingDotsInFourDots.Count - 1; ii++)
                            drawingDots.Add(drawingDotsInFourDots[ii]);
                    }
                };
                posCol.Clear();
                foreach (Vector3 vect in pos)
                {
                    posCol.Add(vect);
                }
                ListLR[ListLR.Count - 1].GetComponent<LineRenderer>().numPositions = drawingDots.ToArray().Length;
                ListLR[ListLR.Count - 1].GetComponent<LineRenderer>().SetPositions(drawingDots.ToArray());
                ListLR[ListLR.Count - 1].GetComponent<EdgeCollider2D>().points = posCol.ToArray();
            }
        }
    }
    public void OnFingerDown(LeanFinger finger)
    {
        ListLR.Add(GameObject.Instantiate(LineRenderer));

    }

    public void OnFingerUp(LeanFinger finger)
    {
        pos = new List<Vector3>();
        posCol = new List<Vector2>();

    }

    private List<Vector3> GetDrawingDots(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        List<Vector3> drawingDots = new List<Vector3>();
        for (float t = 0.0f; t <= 1.0F; t += 0.2f)
        {
            drawingDots.Add(CalculateBezierPoint(t, p0, p1, p2, p3));
        }


        return drawingDots;
    }
    private List<Vector3> GetDrawingDots(Vector3 p0, Vector3 p1, Vector3 p2)
    {
        List<Vector3> drawingDots = new List<Vector3>();
        for (float t = 0.0f; t <= 1.0F; t += 0.2f)
        {
            drawingDots.Add(CalculateBezierPoint(t, p0, p1, p2));
        }


        return drawingDots;
    }
    private List<Vector3> GetDrawingDots(Vector3 p0, Vector3 p1)
    {
        List<Vector3> drawingDots = new List<Vector3>();
        for (float t = 0.0f; t <= 1.0F; t += 0.2f)
        {
            drawingDots.Add(CalculateBezierPoint(t, p0, p1));
        }


        return drawingDots;
    }
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
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

        return p;
    }
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; //first term

        p += 2 * u * t * p1; //second term
        p += tt * p2; //third term

        return p;
    }
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        float u = 1 - t;

        Vector3 p = u * p0; //first term

        p += t * p1; //second term

        return p;
    }
}
