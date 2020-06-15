using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("General")]
    public GameObject SpawnObject;
    public float SpawnTime;
    public int SpawnAmount;

    private float LastSpawn;

    private void Update()
    {
        if(Time.time - LastSpawn > SpawnTime && SpawnAmount > 0)
        {
            if(SpawnObject)
            {
                Instantiate(SpawnObject, transform.position, Quaternion.identity);
                LastSpawn = Time.time;
                SpawnAmount--;
            }
        }
    }
}
