using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelLogic : MonoBehaviour
{

    private Vector2 startPosition;
    private Rigidbody2D rb;
    private Vector2 normalGravity;
    private Vector2 VelocityAtStartAcceleration;
    private float StrengthForAcceleration;
    private float TimeInAcceleration;
    private int FrequencyAcceleration;

    private int maxVelocity = 30;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        normalGravity = Physics2D.gravity;
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        StopRigidbodySimulation();
        GeneralLogic.StartSimulationEvent += StartSimulation;
        GeneralLogic.ResetSimulationEvent += ResetSimulation;
        GeneralLogic.StopSimulationEvent += ResetSimulation;
    }

    private void Update()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        if (Mathf.Abs(transform.position.x) > 150 || Mathf.Abs(transform.position.x) > 150)
            GeneralLogic.ResetSimulationEvent();
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        SetGravity(normalGravity);
        GeneralLogic.StartSimulationEvent -= StartSimulation;
        GeneralLogic.ResetSimulationEvent -= ResetSimulation;
        GeneralLogic.StopSimulationEvent -= ResetSimulation;
    }

    public void StartSimulation()
    {
        rb.simulated = true;
        Debug.Log("Normal Gravity" + normalGravity);
    }

    public void ResetSimulation()
    {
        Debug.Log("Normal Gravity" + normalGravity);
        SetGravity(normalGravity);
        transform.position = startPosition;
        StopRigidbodySimulation();
    }

    public void ResetSimulationInNewPosition()
    {
        StopRigidbodySimulation();
        startPosition = transform.position;
    }
    public void AddForce(Vector2 force)
    {
        rb.AddForce(force);
    }

    public void AddVelocity(float strength, int frequance, float time)
    {
        VelocityAtStartAcceleration = rb.velocity;
        StrengthForAcceleration = strength;
        TimeInAcceleration = time;
        FrequencyAcceleration = frequance;
        StartCoroutine("Acceleration");
    }

    public void AddVelocity(Vector2 Velocity)
    {
        rb.velocity = Velocity;
    }

    private void StopRigidbodySimulation()
    {
        rb.simulated = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    public void SetGravity(Vector2 gravity)
    {
        Physics2D.gravity = gravity;
    }

    public Vector2 getVelocity()
    {
        return rb.velocity;
    }

    IEnumerator Acceleration()
    {
        for (int i = 1; i <= FrequencyAcceleration; i++)
        {
            rb.velocity = Vector2.Lerp(VelocityAtStartAcceleration,
                VelocityAtStartAcceleration * StrengthForAcceleration, (float)i / (float)FrequencyAcceleration);            
            yield return new WaitForSeconds(TimeInAcceleration);
        }
    }

    public void MoveToPortal(Vector3 pos)
    {
        StopRigidbodySimulation();
        StartCoroutine(SmoothMoveTo(pos));
    }

    IEnumerator SmoothMoveTo(Vector3 pos)
    {
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 5);
        yield return new WaitForEndOfFrame();
        if (Vector3.Distance(transform.position, pos) > 0.05)
            StartCoroutine(SmoothMoveTo(pos));
    }
}
