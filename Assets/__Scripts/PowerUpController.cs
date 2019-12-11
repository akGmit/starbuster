using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PowerUpController : MonoBehaviour
{
    [SerializeField]
    private List<PowerUp> powerUps;

    public bool LevelInProgress = false;

    private List<PowerUp> levelPowerUps;
    private float spawnRate;
    private int spawnDelay = 5;
    private LevelSettings settings;
    private int spawnIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        settings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
        spawnRate = settings.SpawnRate;
        SpawnPowerUp();
    }

    public void SpawnPowerUp()
    {
        InvokeRepeating("Spawn", spawnDelay, spawnRate);
    }

    public void StopSpawning()
    {
        CancelInvoke();
    }

    private void Spawn()
    { 
        var points = Utilities.ShuffledStack.CreateShuffledStack<SpawnPoint>(GameObject.Find("Spawners").GetComponentsInChildren<SpawnPoint>().ToList());

        if (spawnIndex > powerUps.Count - 1)
            spawnIndex = 0;

        var powerUp = powerUps[spawnIndex];

        points.First().SpawnPowerUp(powerUp);

        spawnIndex++;
    }

    private void OnEnable()
    {
        LevelController.BossSpawnedEvent += HandleBossSpawnedEvent;
    }

    private void OnDisable()
    {
        LevelController.BossSpawnedEvent -= HandleBossSpawnedEvent;
        StopSpawning();
    }

    private void HandleBossSpawnedEvent()
    {
        StopSpawning();
    }
}
