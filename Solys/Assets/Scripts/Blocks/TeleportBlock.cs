using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBlock : MonoBehaviour {

	public GameObject ExitBlock;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody)
        {
           other.transform.position = ExitBlock.transform.position;
        }

    }
}
