using System.Collections;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("General")]
    public float MovementSpeed;
    public GameObject Target;
    public bool ShowDebug;
    public float StoppingDistance;
    public float FlashTime;
    public int MaxHealth;

    private Animator EnemyAnim;
    private SpriteRenderer Render;
    private AudioSource Aud;
    private bool IsMoving = false, MovingRight = false, MovingUp = false;
    private float LastFlash;
    private void Start()
    {
        EnemyAnim = GetComponent<Animator>();
        Render = GetComponent<SpriteRenderer>();
        Aud = GetComponent<AudioSource>();
    }
    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Hero")
        {
            DamageHero(collision.gameObject);
        }
    }

    private void DamageHero(GameObject hero)
    {
        UnityEngine.Vector2 direction = hero.transform.position - transform.position;
        Debug.DrawLine(transform.position, direction, Color.red);
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * 1000f);
    }

    private void OnDrawGizmos()
    {
        if(ShowDebug && Target)
        {
            Gizmos.DrawWireSphere(transform.position, StoppingDistance);
        }
    }

    private bool CheckStoppingDistance()
    {
        if(UnityEngine.Vector2.Distance(transform.position, Target.transform.position) <= StoppingDistance)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void CheckDamageHit()
    {
        UnityEngine.Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(UnityEngine.Vector3.Distance(new UnityEngine.Vector3(mousePos.x, mousePos.y, transform.position.z), transform.position) < StoppingDistance)
        {
            if(Aud)
            {
                Aud.Play();
            }
            LastFlash = Time.time;
            DamageEnemy();
        }
    }

    private void DamageEnemy()
    {
        if(MaxHealth - 10 <= 0)
        {
            Destroy(transform.gameObject);
        } else
        {
            MaxHealth -= 10;
        }
    }
}
