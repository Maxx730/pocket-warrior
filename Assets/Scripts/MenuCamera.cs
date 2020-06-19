using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [Header("General")]
    public float CameraSpeed;

    private Collider2D SceneDimens;
    private int direction = 1;

    private void Start()
    {
        SceneDimens = GameObject.Find("Doors").GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(transform.position.y >= SceneDimens.bounds.size.y / 1.5) {
            direction = -1;
        } else if(transform.position.y <= 0) {
            direction = 1;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * CameraSpeed * direction), transform.position.z);
    }
}
