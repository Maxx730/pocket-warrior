using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private ParticleSystem parts;

    void Start()
    {
        parts = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            parts.Emit(100);
        }

        if(transform.gameObject.tag == "Sign")
        {
            GetComponent<Animator>().Play("signhit");
        }
    }
}
