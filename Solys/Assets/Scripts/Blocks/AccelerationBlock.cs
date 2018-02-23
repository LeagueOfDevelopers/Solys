using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationBlock : MonoBehaviour
{

    public float TimeInAcceleration;
    public int FrequencyAcceleration;
    public float AccelStrength;
	// Use this for initialization
	void Start () {
		
	}
	
	

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody)
        {
            PlayAnim();
            other.gameObject.GetComponent<WheelLogic>().AddVelocity(AccelStrength,FrequencyAcceleration,TimeInAcceleration);
        }

    }

    private void PlayAnim()
    {
        Transform ps = transform.Find("PS");
        Debug.Log(ps);
        if (ps)
        {

            ps.gameObject.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            ParticleSystem innerPS = GetComponent<ParticleSystem>();
            if (innerPS)
                innerPS.Play();
        }
    }
}
