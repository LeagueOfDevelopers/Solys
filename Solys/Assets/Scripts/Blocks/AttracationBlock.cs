using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttracationBlock : MonoBehaviour
{

    public float RadiusForAttraction;
    private bool inRange;
    public GameObject wheel;
    public float AttracionStrength;
    private float DistanceBetweenPlayerAndBlock;
    // Use this for initialization
	void Start ()
	{
	    inRange = false;
	    GetComponent<CircleCollider2D>().radius = RadiusForAttraction;
	}
	
	// Update is called once per frame
	void Update () {
	    if (inRange)
	    {
	        DistanceBetweenPlayerAndBlock = Vector2.Distance(transform.position, wheel.transform.position);
	        if (DistanceBetweenPlayerAndBlock <= RadiusForAttraction)
	        {
                Vector3 Direction = transform.position - wheel.transform.position;

	            Vector3 Attraction = Direction.normalized * AttracionStrength * (DistanceBetweenPlayerAndBlock) / RadiusForAttraction;

                //wheel.transform.Translate(Attraction,Space.World);
                wheel.GetComponent<WheelLogic>().AddForce(Attraction);

	        }
	    }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            inRange = true;
        GetComponent<ParticleSystem>().Play();

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            inRange = false;
        GetComponent<ParticleSystem>().Stop();
    }
}
