using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    [Header("General")]
    public int Hitpoints;
    public GameObject Loss;
    public float CoinOffset;

    private float lastDamage;
    private SpriteRenderer renderer;
    private Text goldText;

    private void Start()
    {
        renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        goldText = GameObject.Find("PlayerGold").GetComponent<Text>();
        goldText.text = IntToSixString(Hitpoints);
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
        Hitpoints -= amount;
        goldText.text = IntToSixString(Hitpoints);
        Instantiate(Loss, new Vector3(transform.position.x, transform.position.y + CoinOffset, transform.position.z), Quaternion.identity);
    }

    public string IntToSixString(int amount) {
        int length = amount.ToString().Length;
        string finalString = "";
        for(int i = 0;i < 6 - amount.ToString().Length;i++) {
            finalString += "0";
        }
        return finalString + amount.ToString();
    }
}
