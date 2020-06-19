using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    [Header("General")]
    public GameObject Loss;
    public float CoinOffset;

    private float lastDamage;
    private SpriteRenderer renderer;
    private GameController controller;

    private void Start()
    {
        renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if(Time.time - lastDamage < 0.2f) {
            renderer.color = Color.red;
        } else {
            renderer.color = Color.white;
        }
    }

    public void TakeDamage(int amount) {
        lastDamage = Time.time;
        controller.RemoveGold(5);
    }
}
