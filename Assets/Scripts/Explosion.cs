using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("General")]
    public float ExplosionRadius;

    private CircleCollider2D CircCollide;

    private void Start()
    {
        CircCollide = GetComponent<CircleCollider2D>();
        CircCollide.radius = ExplosionRadius;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<Enemy>().ApplyExplosiveReaction(transform.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<Enemy>().ApplyExplosiveReaction(transform.gameObject);
        }
    }
}
