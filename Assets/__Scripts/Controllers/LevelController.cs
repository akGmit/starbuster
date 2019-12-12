using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Level controller script. 
/// Controls level specific gameplay.
/// </summary>
public class LevelController : MonoBehaviour
{
    #region serialized fields
    [SerializeField]
    private HighScoreUtils highScore;
    #endregion  

    private Player player;
    private LevelSettings settings;
    private IList<SpawnPoint> spawnPoints;
    public bool levelInProgress = false;
   
    void Start()
    {
        settings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
        levelInProgress = true;
        player = GameObject.Find("Player").GetComponent<Player>();
        Time.timeScale = 1;
    }

    void Update()
    {
        // If level is in progress call Timer method to follow time
        if (levelInProgress)
        {
            Timer();
        }
    }
    
    /// <summary>
    /// Method following time since level start and if it has reached level time limit.
    /// IF true - stop spawning basic enemies.
    /// After all basic enemies have been destroyed - spawn boss.
    /// </summary>
    private void Timer()
    {
        if (Time.timeSinceLevelLoad > settings.LevelTime)
        {
            StopBasicEnemySpawning();
            if (GameObject.Find("EnemyParent").GetComponentsInChildren<Enemy>().Length == 0)
            {
                levelInProgress = false;
                SpawnBoss();
            }
        }
    }

    /// <summary>
    /// Get reference to all game spawnpoints and cancel InvokeRepeating method for each.
    /// </summary>
    private void StopBasicEnemySpawning()
    {
        spawnPoints = GameObject.Find("Spawners").GetComponentsInChildren<SpawnPoint>();
        spawnPoints.ToList().ForEach(s =>
        {
            s.CancelInvoke();
        });
    }

    // Spawn boss and publish boss spawned event
    private void SpawnBoss()
    {
        GameObject.Find("EnemyParent").GetComponent<EnemyParent>().SpawnBoss();
        PublishBossSpawnedEvent();
    }

    // Show level finished UI
    private void ShowLevelFinished()
    {
        GameObject.Find("InGameMenu").GetComponent<LevelFinished>().LevelFinishedMenu(player.Score);
        player.Score = 0;
    }

    #region Event handlers
    private void HandleEnemyKilledEvent(Enemy enemy)
    {
        player.Score += enemy.ScoreValue;
        if (enemy.CompareTag("Boss"))
        {
            ShowLevelFinished();
        }
    }

    private void HandlePlayerKilledEvent()
    {
        Player.PlayerKilledEvent -= HandlePlayerKilledEvent;
        ShowLevelFinished();
    }
    #endregion

    private void OnEnable()
    {
        Enemy.EnemyKilledEvent += HandleEnemyKilledEvent;
        Player.PlayerKilledEvent += HandlePlayerKilledEvent;
    }

    private void OnDisable()
    {
        Enemy.EnemyKilledEvent -= HandleEnemyKilledEvent;
        Player.PlayerKilledEvent -= HandlePlayerKilledEvent;
    }

    public delegate void BossSpawned();
    public static BossSpawned BossSpawnedEvent;

    private void PublishBossSpawnedEvent() => BossSpawnedEvent?.Invoke();
}
