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

    }

    private void Update()
    {
        Cursor.visible = false;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameCursor.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
}
