using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBlock : MonoBehaviour {

    public float timeForReSpawn;
    private GameObject GameObjectWithGeneralLogic;
    private bool isTimeStop = false;
	// Use this for initialization
	void OnEnable () {
        GameObjectWithGeneralLogic = GameObject.Find("Main Camera");
        GeneralLogic.ResetSimulationEvent += ResetSimulation;
        GeneralLogic.StartSimulationEvent += StartSimulation;
    }

    private void OnDisable()
    {
        GeneralLogic.ResetSimulationEvent -= ResetSimulation;
        GeneralLogic.StartSimulationEvent -= StartSimulation;
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
            GetComponent<ParticleSystem>().Play();
            isTimeStop = true;
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
