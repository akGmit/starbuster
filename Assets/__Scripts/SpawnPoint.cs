using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

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
    [Header("WayPoint Array")]
    private Transform[] waypoints;

    private Stack<SpawnPoint> spawnStack;

    private IList<SpawnPoint> spawnPoints;

    private GameObject enemyParent;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.Find("Spawners").GetComponentsInChildren<SpawnPoint>();
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
        spawnStack = ShuffledStack.CreateShuffledStack(spawnPoints);

        InvokeRepeating("Spawn", spawnDelay, spawnRate);
    }

    private void Spawn()
    {

        if (spawnStack.Count == 0)
        {
            spawnStack = ShuffledStack.CreateShuffledStack(spawnPoints);
            Debug.Log(spawnStack);
        }

        var currentPoint = spawnStack.Pop();
        Debug.Log(currentPoint);
        // create an enemyPrefab
        var enemy = Instantiate(enemyPrefab, enemyParent.transform);
            // set its position to the DockSpawner
            enemy.transform.position = currentPoint.transform.position;
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
