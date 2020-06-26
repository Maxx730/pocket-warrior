using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("General")]
    public GameObject GameCursor;
    public float AttractionRadius;
    public int goldAmount;
    public float cameraReturnTime;

    [Header("UI")]
    public Text ArrowCount;

    [Header("Weapons")]
    public List<GameObject> Arrows;
    public float ArrowSpread;
    public int ArrowAmount;

    private Text goldText;
    private Sound soundSource;
    private LineRenderer lineRenderer;
    private Vector3 FirstPosition;
    private Vector3 SecondPosition;
    private GameObject UiArrow;
    public float shotPower = 0;
    private Vector3 startingPosition;
    private bool backToStart = false;
    private Text ShotPowerText;

    private void Start()
    {
        UiArrow = GameObject.Find("UiArrow");
        UiArrow.SetActive(false);
        soundSource = GameObject.Find("SoundSource").GetComponent<Sound>();
        lineRenderer = GetComponent<LineRenderer>();
        ArrowCount.text = ArrowAmount.ToString();
        ShotPowerText = GameObject.Find("ShotPower").GetComponent<Text>();
    }

    private void OnDrawGizmos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Gizmos.DrawWireSphere(mousePos, AttractionRadius);
    }

    private void Update()
    {

        //Only do arrow logic if there are arrows left to fire.
        if(ArrowAmount >  0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lineRenderer.enabled = true;
                FirstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                UiArrow.SetActive(true);
            }

            if (Input.GetMouseButton(0))
            {
                SecondPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                UiArrow.transform.position = new Vector3(SecondPosition.x, SecondPosition.y, 0);
                Vector3 _direction = FirstPosition - UiArrow.transform.position;
                float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                shotPower = Vector2.Distance(FirstPosition, UiArrow.transform.position);
                UiArrow.transform.rotation = Quaternion.AngleAxis(_angle - 90f, Vector3.forward);
                ShotPowerText.text = shotPower.ToString();
                SetLineColor(false);
            }

            if (Input.GetMouseButtonUp(0))
            {
                UiArrow.SetActive(false);
                CreateArrows();
                ArrowAmount--;
                ArrowCount.text = ArrowAmount.ToString();
                SetLineColor(false, false);
            }

            DrawDrag();
        }
    }

    private void SetLineColor(bool hasPower, bool touchUp = true)
    {
        if(hasPower)
        {
            lineRenderer.startColor = new Color32(255, 0, 0, 65);
            lineRenderer.endColor = new Color32(255, 0, 0, 65); ;
        } else
        {
            lineRenderer.enabled = touchUp;
            lineRenderer.startColor = new Color32(255, 255, 255, 65);
            lineRenderer.endColor = new Color32(255, 255, 255, 65);
        }
    }

    private void DrawDrag()
    {
        //Draw the dragged line
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

    public void CreateArrows()
    {
        soundSource.BowRelease.Play();
        List<GameObject> arrows = new List<GameObject>();

        arrows.Add(Instantiate(Arrows[Random.Range(0,Arrows.Count)], new Vector3(SecondPosition.x, SecondPosition.y, 0), UiArrow.transform.rotation));

        //Disable all collisions between each of the shot arrows.
        for(int i = 0; i < arrows.Count; i++)
        {
            for(int k = 0;k < arrows.Count;k++)
            {
                Physics2D.IgnoreCollision(arrows[i].GetComponent<Collider2D>(), arrows[k].GetComponent<Collider2D>());
            }
        }
    }

    public void RemoveGold(int amt) {
        goldAmount -= amt;
        goldText.text = Util.IntToSixString(goldAmount);
    }

    public void SetBackToStart(bool value)
    {
        backToStart = value;
    }

    public void AddArrow(int prize)
    {
        ArrowAmount += prize;
        ArrowCount.text = ArrowAmount.ToString();
    }
}
