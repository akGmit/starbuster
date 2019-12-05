using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
/// <summary>
/// Class representing individual enemy starting spawn point. 
/// </summary>
public class SpawnPoint : MonoBehaviour
{

    // == fields ==
    #region Serialized fields
    [SerializeField]
    private float spawnDelay = 0.25f;

    [SerializeField]
    private float spawnRate = 0.3f;

    [SerializeField]
    private float enemyStartSpeed = 2f;

    [SerializeField]
    private Enemy enemyPrefab;

    [SerializeField]
    private Enemy bossPrefab;

    [SerializeField]
    [Header("WayPoint Array")]
    private Transform[] waypoints;
    #endregion

    #region private members
    private Stack<SpawnPoint> spawnStack;
    private IList<SpawnPoint> spawnPoints;
    private GameObject enemyParent;
    private Enemy boss;
    private WaypointFollower follower;
    #endregion

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
          
        }

        var currentPoint = spawnStack.Pop();
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

    public void BossSpawn()
    {
        boss = Instantiate(bossPrefab, enemyParent.transform);

        InvokeRepeating("BossMove", 2, 1);
        //BossMove();
    }

    private void BossMove()
    {
        if (spawnStack.Count == 0)
        {
            spawnStack = ShuffledStack.CreateShuffledStack(spawnPoints);
        }

        var currentPoint = spawnStack.Pop();
        var boss = Instantiate(bossPrefab, enemyParent.transform);
        boss.transform.position = currentPoint.transform.position;
        // give it a path to follow
        follower = boss.GetComponent<WaypointFollower>();
        // add the array of Dock points to an arrray in the enemy
        foreach (var point in waypoints)
        {
            follower.AddPointToFollow(point.position);
        }
        // set a speed for the enemy
        follower.Speed = enemyStartSpeed;
    }
}
