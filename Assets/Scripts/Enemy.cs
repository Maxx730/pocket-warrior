using System.Collections;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [Header("General")]
    public float MovementSpeed;
    public float AttackFrequency;
    public float AgroDistance;
    public int EnemyHitpoints;
    public GameObject[] Rewards;

    private Animator EnemyAnim;
    private SpriteRenderer Render;
    private AudioSource Aud;
    private AIDestinationSetter AiDest;
    private AIPath AIPath;
    private GameObject Goal;
    private Rigidbody2D RBody;
    private ParticleSystem DamagePart;

    private bool agro = false;
    private float lastAttack;
    private float lastDamage;
    public float lastExplosion;

    private void Start()
    {
        EnemyAnim = GetComponent<Animator>();
        Render = GetComponent<SpriteRenderer>();
        Aud = GetComponent<AudioSource>();
        AiDest = GetComponent<AIDestinationSetter>();
        AIPath = GetComponent<AIPath>();
        RBody = GetComponent<Rigidbody2D>();
        DamagePart = transform.GetChild(0).GetComponent<ParticleSystem>();
        Goal = GameObject.Find("EnemyGoal");

        AiDest.target = Goal.transform;
    }
    private void Update()
    {
        //Make sure we are in agro mode and we are only attacking once second.
        if(agro && Time.time - lastAttack > 1f) {
            AttackGoal();
        }

        if(CheckDistanceFromGoal() && Time.time - lastExplosion > 1f) {
            AIPath.canMove = true;
            AiDest.target = Goal.transform;
            agro = false;
        }

        if(Time.time - lastDamage < .15f) {
            Render.color = Color.red;
        } else {
            Render.color = Color.white;
        }
    }

    //If the slime is somehow too far away from the goal, reactivate path finding and move back towards the goal.
    private bool CheckDistanceFromGoal() {
        if(Goal) {
            float distance = UnityEngine.Vector2.Distance(transform.position, Goal.transform.position);
            if(distance > 3) {
                return true;
            }
            return false;
        }
        return false;
    }

    private void AttackGoal() {
        UnityEngine.Vector2 direction = Goal.transform.position - transform.position;
        RBody.AddForce(direction * 1000f);
        lastAttack = Time.time;
        Aud.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.name == "EnemyGoal") {
            //Since we are now close enough to the goal, we can now start to attack the goal.
            agro = true;
            AiDest.target = null;
            AIPath.canMove = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "EnemyGoal") {
            Goal.GetComponent<Goal>().TakeDamage(5);
        }
    }
    public void TurnOffAi() {
        AiDest.target = null;
        AIPath.canMove = false;
    }

    public void TakeDamage(int value) {
        lastDamage = Time.time;

        if(EnemyHitpoints - value <= 0) {
            if(Rewards.Length > 0) {
                for(int i = 0;i < Rewards.Length;i++) {
                    Instantiate(Rewards[i], transform.position, UnityEngine.Quaternion.identity);
                }
            }
            Destroy(transform.gameObject);
        } else {
            EnemyHitpoints -= value;
            DamagePart.Play();
        }
    }
}
