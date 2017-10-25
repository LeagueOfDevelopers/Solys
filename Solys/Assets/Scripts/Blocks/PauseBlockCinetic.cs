using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBlockCinetic : MonoBehaviour {

    private Vector2 velocity = Vector2.zero;
    public float timeForReSpawn;
    private GameObject wheel;
    private GameObject GameObjectWithGeneralLogic;
    private bool isTimeStop = false;
    // Use this for initialization
    void OnEnable()
    {
        GeneralLogic.ResetSimulationEvent += ResetSimulation;
        GeneralLogic.StartSimulationEvent += StartSimulation;
        wheel = GameObject.Find("Wheel");
        GameObjectWithGeneralLogic = GameObject.Find("Main Camera");
    }

    private void OnDisable()
    {
        GeneralLogic.ResetSimulationEvent -= ResetSimulation;
        GeneralLogic.StartSimulationEvent -= StartSimulation;
    }

    void StartSimulation()
    {
        if (isTimeStop)
        {
            isTimeStop = false;
            wheel.GetComponent<WheelLogic>().AddVelocity(velocity);
        }
    }

    void ResetSimulation()
    {
        setActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            GetComponent<ParticleSystem>().Play();
            isTimeStop = true;
            velocity = wheel.GetComponent<WheelLogic>().getVelocity();
            StartCoroutine("ReSpawnBlock");
            GameObjectWithGeneralLogic.GetComponent<GeneralLogic>().PauseBlockActivated();
        }
    }

    IEnumerator ReSpawnBlock()
    {
        setActive(false);
        while (isTimeStop)
            yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(timeForReSpawn);
        setActive(true);
    }
    void setActive(bool active)
    {        
        GetComponent<SpriteRenderer>().enabled = active;
        GetComponent<CircleCollider2D>().enabled = active;
    }
}
