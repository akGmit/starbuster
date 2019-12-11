using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class LevelController : MonoBehaviour
{
    [SerializeField]
    private HighScoreUtils highScore;

    [SerializeField]
    private LevelFinished levelFinishedMenu;

    private Player player;

    public string Level;

    private LevelSettings settings;
    private IList<SpawnPoint> spawnPoints;
    public bool levelInProgress = false;
   
    void Start()
    {
        settings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
        Level = settings.Name;
        levelInProgress = true;
        player = GameObject.Find("Player").GetComponent<Player>();
        Time.timeScale = 1;
    }

    void Update()
    {
        if (levelInProgress)
        {
            Timer();
        }
    }

    private void Timer()
    {
        if (Time.timeSinceLevelLoad > 10)
        {
            StopBasicEnemySpawning();
            if (GameObject.Find("EnemyParent").GetComponentsInChildren<Enemy>().Length == 0)
            {
                levelInProgress = false;
                SpawnBoss();
            }
        }
    }

    private void StopBasicEnemySpawning()
    {
        spawnPoints = GameObject.Find("Spawners").GetComponentsInChildren<SpawnPoint>();
        spawnPoints.ToList().ForEach(s =>
        {
            s.CancelInvoke();
        });
    }

    private void SpawnBoss()
    {
        GameObject.Find("EnemyParent").GetComponent<EnemyParent>().SpawnBoss();
        PublishBossSpawnedEvent();
    }

    private void ShowLevelFinished()
    {
        levelFinishedMenu.LevelFinishedMenu(player.Score);
    }

    #region Event handlers
    private void HandleEnemyKilledEvent(Enemy enemy)
    {
        player.Score += enemy.ScoreValue;
        if (enemy.CompareTag("Boss"))
        {
            levelInProgress = false;
            ShowLevelFinished();
        }
    }

    private void HandlePlayerKilledEvent()
    {
        levelInProgress = false;
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
