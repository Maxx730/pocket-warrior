using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("General")]
    public int ArrowSpeed;
    public int ArrowDamage;
    private Rigidbody2D rigidbody;
    private Collider2D collider;
    private bool hasHit = false;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(!hasHit) {
            rigidbody.AddForce(transform.up * ArrowSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy") {
            if(collision.gameObject.tag == "Enemy" && !hasHit) {
                transform.parent = collision.gameObject.transform;
                Destroy(transform.gameObject);
                collision.gameObject.GetComponent<Enemy>().TakeDamage(ArrowDamage);
            }

            hasHit = true;
            rigidbody.velocity = new Vector2(0, 0);
            transform.GetChild(0).gameObject.SetActive(false);
            rigidbody.isKinematic = true;
            collider.isTrigger = true;
            Destroy(transform.gameObject, 3f);
        }
    }
}
