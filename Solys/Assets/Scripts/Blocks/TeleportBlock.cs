using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBlock : MonoBehaviour
{

    public GameObject ExitBlock;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody)
        {
            
            PlayAnim(gameObject);
            PlayAnim(ExitBlock);
            other.transform.position = ExitBlock.transform.position;
        }

    }

    private void PlayAnim(GameObject obj)
    {
        Transform ps = obj.transform.Find("PS");
        Debug.Log(ps);
        if (ps)
        {

            ps.gameObject.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            ParticleSystem innerPS = obj.GetComponent<ParticleSystem>();
            if (innerPS)
                innerPS.Play();
        }
    }
}
