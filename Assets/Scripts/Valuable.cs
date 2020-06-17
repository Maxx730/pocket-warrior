using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuable : MonoBehaviour
{
    public bool IsCollected = false;
    private Rigidbody2D rigidbody;
    private Vector3 lerpLocation;
    private void Start()
    {
        Vector3 forDir = new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0);
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(forDir * 2f);
        lerpLocation = Camera.main.ScreenToWorldPoint(GameObject.Find("GoldIcon").transform.position);
    }

    private void Update()
    {
        if(IsCollected) {
            MoveToLocation();
        }
    }

    private void MoveToLocation() {
        float distance = Vector2.Distance(transform.position, lerpLocation);

        if(distance <= 1) {
            Destroy(transform.gameObject);
        } else {
            transform.position = Vector3.Lerp(transform.position, lerpLocation, Time.deltaTime * 10f);
        }
    }
}
