using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("General")]
    public GameObject GameCursor;

    [Header("Dungeon Rooms")]
    public List<GameObject> Rooms;

    private GameObject Hero;
    private GameObject MovementTarget;
    private int NextStop = 0;

    private void Start()
    {
        Hero = GameObject.Find("Hero");
        MovementTarget = GameObject.Find("MovementTarget");
    }

    private void Update()
    {
        Cursor.visible = false;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameCursor.transform.position = new Vector3(mousePos.x, mousePos.y, 0);

        if(Input.GetMouseButtonDown(0))
        {
            MovementTarget.SetActive(true);
            MovementTarget.transform.position = new Vector3(mousePos.x, mousePos.y, MovementTarget.transform.position.z);
            Hero.GetComponent<AIDestinationSetter>().target = MovementTarget.transform;
            Hero.GetComponent<AIPath>().canMove = true;
            Hero.GetComponent<Hero>().IsMoving = true;
        }
    }
}
