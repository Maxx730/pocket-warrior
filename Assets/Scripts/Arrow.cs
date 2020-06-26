using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool hasHit = false;
    private Sound sound;
    private GameController controller;
    private GameObject ArrowPreview;
    private GameObject Trail;
    private Text TravelDistance, ShotPower, PrizeValue;
    private float travelDistance, traveledDistance;
    private Vector3 startingPoint;

    public bool windEffect = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        sound = GameObject.Find("SoundSource").GetComponent<Sound>();
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        ArrowPreview = GameObject.Find("ArrowPreviewCamera");
        TravelDistance = GameObject.Find("TravelDistance").GetComponent<Text>();
        PrizeValue = GameObject.Find("PrizeValue").GetComponent<Text>();
        sRenderer.sprite = Normal;
        startingPoint = transform.position;
        CalculateShotDistance(controller.shotPower);
        TravelDistance.text = travelDistance.ToString();
        Trail = transform.GetChild(1).gameObject;
    }

    private void FixedUpdate()
    {
        if(traveledDistance >= travelDistance) {
            Destroy(transform.gameObject);
        }
        else
        {
            if (!hasHit)
            {
                SetArrowScaling();
                traveledDistance = Vector2.Distance(transform.position, startingPoint);
                rigidbody.AddForce(transform.up * ArrowSpeed);
            }
        }
    }

    private void Update()
    {
        ArrowPreview.transform.position = new Vector3(transform.position.x, transform.position.y, ArrowPreview.transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Sign":
                Destroy(transform.gameObject);
                break;
            case "Target":
                transform.parent = collision.gameObject.transform;
                FinishArrow(CalculatePrizeAmount());
                break;
            case "Wall":
                FinishArrow(0);
                break;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(transform.gameObject);
    }

    public void ApplyWind(Vector3 source, float power)
    {
        Vector3 dir = source - transform.position;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, transform.forward), Time.deltaTime * power);
    }

    private void FinishArrow(int prize)
    {
        Trail.GetComponent<TrailRenderer>().enabled = false;
        transform.localScale = new Vector3(1, 1, 1);
        hasHit = true;
        rigidbody.velocity = new Vector2(0, 0);
        rigidbody.angularVelocity = 0;
        rigidbody.isKinematic = true;
        collider.isTrigger = true;
        sRenderer.sprite = InWall;
        PrizeValue.text = "+" + prize.ToString();
        sound.ArrowImpact.Play();
        controller.AddArrow(prize);
        controller.SetBackToStart(true);
    }

    private void CalculateShotDistance(float power)
    {
        travelDistance = power * 10;

    }

    private void SetArrowScaling()
    {
        if (traveledDistance >= travelDistance / 2)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.01f, transform.localScale.y - 0.01f, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.01f, transform.localScale.y + 0.01f, transform.localScale.z);
        }
    }

    private int CalculatePrizeAmount() {
        return (int) Mathf.Ceil(traveledDistance / 10);
    }
}
