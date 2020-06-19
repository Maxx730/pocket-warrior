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
    private LineRenderer lineRenderer;

    private bool TouchDown = false;

    private Vector3 FirstPosition;
    private Vector3 SecondPosition;
    private GameObject UiArrow;

    private void Start()
    {
        goldText = GameObject.Find("PlayerGold").GetComponent<Text>();
        UiArrow = GameObject.Find("UiArrow");
        UiArrow.SetActive(false);
        soundSource = GameObject.Find("SoundSource").GetComponent<Sound>();
        lineRenderer = GetComponent<LineRenderer>();
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
            lineRenderer.enabled = true;
            FirstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TouchDown = true;
            UiArrow.SetActive(true);
        }

        if(Input.GetMouseButton(0)) {
            SecondPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UiArrow.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            Vector3 _direction = FirstPosition - UiArrow.transform.position;
            float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            UiArrow.transform.rotation = Quaternion.AngleAxis(_angle - 90f, Vector3.forward);
        }

        if(Input.GetMouseButtonUp(0)) {
            TouchDown = false;
            UiArrow.SetActive(false);
            Instantiate(Primary, new Vector3(SecondPosition.x, SecondPosition.y, 0), UiArrow.transform.rotation);
            SecondPosition = FirstPosition;
            lineRenderer.enabled = false;
        }

            Vector3[] points = new Vector3[2];
            points[0] = new Vector3(FirstPosition.x, FirstPosition.y, 0);
            points[1] = new Vector3(SecondPosition.x, SecondPosition.y, 0);
            lineRenderer.SetPositions(points);
            lineRenderer.numCornerVertices = 15;
            lineRenderer.numCapVertices = 15;
        
    }

    private void CollectValuables() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] valuables = Physics2D.OverlapCircleAll(new Vector3(mousePos.x, mousePos.y, 0), AttractionRadius);

        if(valuables.Length > 0) {
            for(int i = 0;i < valuables.Length;i++) {
                if(valuables[i].gameObject.tag == "Valuable") {
                    goldAmount += valuables[i].GetComponent<Valuable>().value;
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
