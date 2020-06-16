using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [Header("General")]
    public int MaxHealth;
    public bool IsMoving = false;

    //UI Objects
    private Slider healthIndicator;

    private AIDestinationSetter AISet;
    private AIPath AiPath;
    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    private float lastDamage;

    private void Start()
    {
        AISet = GetComponent<AIDestinationSetter>();
        AiPath = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        healthIndicator = GameObject.Find("HeroHealthIndicator").GetComponent<Slider>();
        healthIndicator.maxValue = MaxHealth;
        healthIndicator.value = MaxHealth;
    }

    private void Update()
    {
        if(Time.time - lastDamage < 0.2f) {
            renderer.color = Color.red;
        } else {
            renderer.color = Color.white;
        }
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

    public void TakeDamage(int value, Vector2 direction, int power)
    {
        if(MaxHealth - value > 0)
        {
            rb.AddForce(direction * power);
            lastDamage = Time.time;
            MaxHealth -= value;
            healthIndicator.value = MaxHealth;
        } else
        {
            Destroy(transform.gameObject);
        }
    }
}
