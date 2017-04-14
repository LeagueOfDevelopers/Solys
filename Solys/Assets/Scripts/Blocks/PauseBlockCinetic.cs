using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBlockCinetic : MonoBehaviour {

    private Vector2 velocity = Vector2.zero;
    public float timeForReSpawn;
    public GameObject wheel;
    public GameObject GameObjectWithGeneralLogic;
    private bool isTimeStop = false;
    // Use this for initialization
    void Start()
    {
        GeneralLogic.ResetSimulationEvent += ResetSimulation;
        GeneralLogic.StartSimulationEvent += StartSimulation;
    }

    // Update is called once per frame
    void Update()
    {

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
            GameObjectWithGeneralLogic.GetComponent<GeneralLogic>().StopSimulation();
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
