using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("General")]
    public int ArrowSpeed;
    public int ArrowDamage;
    public Sprite Normal;
    public Sprite InWall;

    private Rigidbody2D rigidbody;
    private SpriteRenderer sRenderer;
    private Collider2D collider;
    private bool hasHit = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        sRenderer = GetComponent<SpriteRenderer>();

        sRenderer.sprite = Normal;
    }

    private void Update()
    {
        if(!hasHit) {
            rigidbody.AddForce(transform.up * ArrowSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Sign":
                Destroy(transform.gameObject);
                break;
            case "Target":
                hasHit = true;
                rigidbody.velocity = new Vector2(0, 0);
                rigidbody.isKinematic = true;
                collider.isTrigger = true;
                sRenderer.sprite = InWall;
                transform.parent = collision.gameObject.transform;
                GetComponent<TrailRenderer>().Clear();
                Destroy(transform.gameObject, 3f);
                break;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(transform.gameObject);
    }
}
