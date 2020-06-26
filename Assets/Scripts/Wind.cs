using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float DirectionLeft;
    public float ForceAmount;

    private Vector3 sourcePosition;

    private void Start()
    {
        sourcePosition = transform.GetChild(0).position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Projectile" && !collision.gameObject.GetComponent<Arrow>().hasHit)
        {
            collision.gameObject.GetComponent<Arrow>().ApplyWind(sourcePosition, ForceAmount);
        }
    }
}
