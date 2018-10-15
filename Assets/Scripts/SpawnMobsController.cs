using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMobsController : MonoBehaviour {
    public GameObject[] mobPrefab;
    public Transform[] spawnPoints;
    public float spawnDelay;

    private float timer;
	
	void Update ()
    {
        SpawnMob();
	}

    private void SpawnMob()
    {
        if(Time.time > timer)
        {
            int randomPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(mobPrefab[0], spawnPoints[randomPoint].position, Quaternion.identity);

            timer = Time.time + spawnDelay;
        }
    }
}
