using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBlock : MonoBehaviour {

    public float timeForReSpawn;
    public GameObject GameObjectWithGeneralLogic;
    private bool isTimeStop = false;
	// Use this for initialization
	void Start () {
        GeneralLogic.ResetSimulationEvent += ResetSimulation;
        GeneralLogic.StartSimulationEvent += StartSimulation;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartSimulation()
    {
        if (isTimeStop) isTimeStop = false;
    }

    void ResetSimulation()
    {
        setActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.tag.Equals("Player"))
        {
            isTimeStop = true;
            StartCoroutine("ReSpawnBlock");
            GameObjectWithGeneralLogic.GetComponent<GeneralLogic>().StopSimulation();
        }
    }

    IEnumerator ReSpawnBlock()
    {
        setActive(false);
        while (isTimeStop)
        yield return new WaitForSeconds(timeForReSpawn);
        setActive(true);
    }
    void setActive(bool active)
    {
        GetComponent<SpriteRenderer>().enabled = active;
        GetComponent<CircleCollider2D>().enabled = active;
    }
}
