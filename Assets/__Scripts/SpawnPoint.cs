using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // similar to point spawner - spawn method, spawndelay, rate

    // == fields ==
    [SerializeField]
    private float spawnDelay = 0.25f;

    [SerializeField]
    private float spawnRate = 0.3f;

    [SerializeField]
    private float enemyStartSpeed = 2f;

    [SerializeField]
    private Enemy enemyPrefab;

    [SerializeField]
    private int enemyLimit = 5;

    private int enemyCount = 0;

    [SerializeField]
    [Header("WayPoint Array")]
    private Transform[] waypoints;

    private GameObject enemyParent;
    // Start is called before the first frame update
    void Start()
    {
        // get the EnemyParent object
        enemyParent = GameObject.Find("EnemyParent");
        if (!enemyParent)
        {
            enemyParent = new GameObject("EnemyParent");
        }
        SpawnRepeating();
    }

    private void SpawnRepeating()
    {
        InvokeRepeating("Spawn", spawnDelay, spawnRate);
    }

    private void Spawn()
    {
        
            // create an enemyPrefab
            var enemy = Instantiate(enemyPrefab, enemyParent.transform);
            // set its position to the DockSpawner
            enemy.transform.position = transform.position;
            // give it a path to follow
            var follower = enemy.GetComponent<WaypointFollower>();
            // add the array of Dock points to an arrray in the enemy
            foreach (var point in waypoints)
            {
                follower.AddPointToFollow(point.position);
            }
            // set a speed for the enemy
            follower.Speed = enemyStartSpeed;

    }

    
}
