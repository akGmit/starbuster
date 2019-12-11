using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;
/// <summary>
/// Class representing individual enemy starting spawn point. 
/// </summary>
public class SpawnPoint : MonoBehaviour
{
    #region Serialized fields
    [SerializeField]
    private float spawnDelay;

    [SerializeField]
    private float spawnRate;

    [SerializeField]
    private float enemyStartSpeed;

    [SerializeField]
    private Enemy enemyPrefab;

    [SerializeField]
    [Header("WayPoint Array")]
    private List<Transform> waypoints;
    #endregion

    #region private members
    private LevelSettings settings;
    private Stack<SpawnPoint> spawnStack;
    private IList<SpawnPoint> spawnPoints;
    private GameObject enemyParent;
    private WaypointFollower follower;
    
    #endregion

    void Start()
    {
        settings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
        spawnPoints = GameObject.Find("Spawners").GetComponentsInChildren<SpawnPoint>();
        

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
        InvokeRepeating("ShuffleWaypoints", 5, 5);
    }

    private void Spawn()
    {
        if (spawnStack.Count == 0)
        {
            spawnStack = ShuffledStack.CreateShuffledStack(spawnPoints);
        }
        var currentPoint = spawnStack.Pop();

        var enemy = Instantiate(enemyPrefab, enemyParent.transform);
        enemy.transform.position = currentPoint.transform.position;

        var follower = enemy.GetComponent<WaypointFollower>();

        foreach (var point in waypoints)
        {
            follower.AddPointToFollow(point.position);
        }

        follower.Speed = enemyStartSpeed;
    }

    private void ShuffleWaypoints()
    { 
        var temp = waypoints.GetRange(0, waypoints.Count - 1);
        temp = ShuffledStack.CreateShuffledStack(temp).ToList();
        temp.Add(waypoints.Last());

        waypoints = temp;
    }   

    public void SpawnPowerUp(PowerUp powerUp)
    {
        var pUp = Instantiate(powerUp);
        pUp.transform.position = spawnStack.Peek().transform.position;
    }
}
