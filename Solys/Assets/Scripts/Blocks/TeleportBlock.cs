using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBlock : MonoBehaviour {

	public GameObject ExitBlock;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody)
        {
            Vector2 direction = new Vector2(ExitBlock.transform.position.x - transform.position.x, ExitBlock.transform.position.y - transform.position.y);
            var velocitySetting = GetComponent<ParticleSystem>().velocityOverLifetime;
            velocitySetting.x = direction.x/3;
            velocitySetting.y = direction.y/3;
            var triggerSetting = GetComponent<ParticleSystem>().trigger;
            triggerSetting.SetCollider(0, ExitBlock.GetComponent<Collider2D>());
            GetComponent<ParticleSystem>().Play();
            ExitBlock.GetComponent<ParticleSystem>().Play();
            other.transform.position = ExitBlock.transform.position;
        }

    }
}
