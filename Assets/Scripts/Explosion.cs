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
        Destroy(transform.gameObject, .25f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy") {

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy") {
             ApplyExplosiveReaction(other.gameObject);
        }
    }

    private void ApplyExplosiveReaction(GameObject target) {
        target.GetComponent<Enemy>().TurnOffAi();
        target.GetComponent<Enemy>().lastExplosion = Time.time;
        UnityEngine.Vector2 direction = transform.position - target.transform.position;
        target.GetComponent<Rigidbody2D>().AddForce(-direction * 50f);
    }
}
