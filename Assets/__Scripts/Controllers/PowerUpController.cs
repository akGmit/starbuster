using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PowerUpController : MonoBehaviour
{
    [SerializeField]
    private List<PowerUp> powerUps;

    [SerializeField]
    private PowerUp firingRate;

    private float spawnRate;
    private int spawnDelay;
    private LevelSettings settings;
    private int spawnIndex = 0;

    void Start()
    {
        settings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
        spawnRate = settings.PowerUpSpawnRate;
        spawnDelay = (int)spawnRate / 2;
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

    /// <summary>
    /// Spawn powerup. 
    /// Get shuffled list of spawnpoints and randomly spawn power up at it.
    /// </summary>
    private void Spawn()
    { 
        var points = Utilities.ShuffledStack.CreateShuffledStack(GameObject.Find("Spawners").GetComponentsInChildren<SpawnPoint>().ToList());

        if (spawnIndex > powerUps.Count - 1)
            spawnIndex = 0;

        var powerUp = Instantiate(powerUps[spawnIndex], points.Last().transform.position, Quaternion.identity);
        spawnIndex++;
    }

    private void OnEnable()
    {
        LevelController.BossSpawnedEvent += HandleBossSpawnedEvent;
        Enemy.EnemyKilledEvent += HandleEnemyKilledEvent;
    }

    private void HandleEnemyKilledEvent(Enemy enemy)
    {
        int rand = UnityEngine.Random.Range(0, 10);
        if(rand == 5)
        {
            Instantiate(firingRate, enemy.transform.position, Quaternion.identity);
        }
    }

    private void OnDisable()
    {
        LevelController.BossSpawnedEvent -= HandleBossSpawnedEvent;
        Enemy.EnemyKilledEvent -= HandleEnemyKilledEvent;
        StopSpawning();
    }

    private void HandleBossSpawnedEvent()
    {
        StopSpawning();
    }
}
