using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("General")]
    public GameObject GameCursor;
    public float AttractionRadius;
    public int goldAmount;

    [Header("Dungeon Rooms")]
    public List<GameObject> Rooms;

    [Header("Weapons")]
    public GameObject Primary;

    private Text goldText;
    private Sound soundSource;

    private void Start()
    {
        goldText = GameObject.Find("PlayerGold").GetComponent<Text>();
        soundSource = GameObject.Find("SoundSource").GetComponent<Sound>();
        goldText.text = Util.IntToSixString(goldAmount);
    }

    private void OnDrawGizmos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Gizmos.DrawWireSphere(mousePos, AttractionRadius);
    }

    private void Update()
    {
        Cursor.visible = false;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameCursor.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        CollectValuables();

        if(Input.GetMouseButtonDown(0)) {
            Instantiate(Primary, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
        }
    }

    private void CollectValuables() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] valuables = Physics2D.OverlapCircleAll(new Vector3(mousePos.x, mousePos.y, 0), AttractionRadius);

        if(valuables.Length > 0) {
            for(int i = 0;i < valuables.Length;i++) {
                if(valuables[i].gameObject.tag == "Valuable") {
                    goldAmount++;
                    goldText.text = Util.IntToSixString(goldAmount);
                    soundSource.CollectCoin.Play();
                    valuables[i].GetComponent<Valuable>().IsCollected = true;
                }
            }
        }
    }

    public void RemoveGold(int amt) {
        goldAmount -= amt;
        goldText.text = Util.IntToSixString(goldAmount);
    }
}
