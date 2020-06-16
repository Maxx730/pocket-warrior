using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gain : MonoBehaviour
{
    [Header("General")]
    public float TossPower;

    private GameObject Coin;
    private Rigidbody2D rigidbody;
    private float startingY;

    private void Start()
    {
        Coin = transform.GetChild(0).gameObject;
        rigidbody = GetComponent<Rigidbody2D>();
        startingY = transform.position.y;

        Vector2 force = new Vector2(Random.Range(-0.2f, 0.2f),1);

        rigidbody.AddForce(force * TossPower);
        rigidbody.gravityScale = 0.75f;
    }

    private void Update()
    {
        if(transform.position.y <= startingY) {
            Destroy(transform.gameObject);
        }
    }
}
