using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [Header("General")]
    public int MaxHealth;
    public bool IsMoving = false;

    private AIDestinationSetter AISet;
    private AIPath AiPath;
    private Rigidbody2D rb;

    private void Start()
    {
        AISet = GetComponent<AIDestinationSetter>();
        AiPath = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "MovementTarget")
        {
            AISet.target = null;
            AiPath.canMove = false;
            IsMoving = false;
            collision.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int value)
    {
        if(MaxHealth - value > 0)
        {
            MaxHealth -= value;
        } else
        {
            Destroy(transform.gameObject);
        }
    }
}
