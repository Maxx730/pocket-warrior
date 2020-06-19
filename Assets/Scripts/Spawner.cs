using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("General")]
    public List<GameObject> SpawnObjects;
    public float SpawnTime;
    public int SpawnAmount;
    public float SpawnRadius;

    private float LastSpawn;

    private void Update()
    {
        if(Time.time - LastSpawn > SpawnTime && SpawnAmount > 0)
        {
            if(SpawnObjects.Count > 0)
            {
                Instantiate(SpawnObjects[Random.Range(-1,SpawnObjects.Count)], transform.position, Quaternion.identity);
                LastSpawn = Time.time;
                SpawnAmount--;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SpawnRadius);
    }

    private Vector3 GetRandomSpawnPoint(Vector3 center) {
        Vector3 spawnPoint = Quaternion.AngleAxis(Random.Range(0.0f,180.0f), Vector3.up) * Vector3.forward;
        return center + spawnPoint;
    }
}
