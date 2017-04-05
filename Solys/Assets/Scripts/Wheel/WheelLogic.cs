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
        GeneralLogic.StopSimulationEvent += StopRigidbodySimulation;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        SetGravity(normalGravity);
        GeneralLogic.StartSimulationEvent -= StartSimulation;
        GeneralLogic.ResetSimulationEvent -= ResetSimulation;
        GeneralLogic.StopSimulationEvent -= StopRigidbodySimulation;
    }

    public void StartSimulation()
    {
        rb.simulated = true;
    }

    public void ResetSimulation()
    {
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

    private void StopRigidbodySimulation()
    {
        rb.simulated = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    public void SetGravity(Vector2 gravity)
    {
        Debug.Log(Physics2D.gravity.magnitude);
        Physics2D.gravity = gravity;
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
}
