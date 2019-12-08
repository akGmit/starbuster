using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    #region Serialized fields

    [SerializeField]
    private HighScoreUtils highScore;

    [SerializeField]
    private Player player;
    #endregion

    private IList<SpawnPoint> spawnPoints;
    private bool levelStarted = false;
    public String level;

    private void Start()
    {
    }

    private void Update()
    {
       if(levelStarted) Timer(); 
    }

    private void Timer()
    {
        if (Time.timeSinceLevelLoad > Levels.Level[level].LevelTime)
        {
            StopBasicEnemySpawning();
            if (GameObject.Find("EnemyParent").GetComponentsInChildren<Enemy>().Length == 0)
            {
                levelStarted = false;
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

    private void SpawnBoss() => GameObject.Find("EnemyParent").GetComponent<EnemyParent>().SpawnBoss();
    

    private void OnEnable()
    {
        Enemy.EnemyKilledEvent += HandleEnemyKilledEvent;
        Player.PlayerKilledEvent += HandlePlayerKilledEvent;
        SceneManager.sceneLoaded += HandleLevelLoadedEvent;
    }

    private void OnDisable()
    {
        Enemy.EnemyKilledEvent -= HandleEnemyKilledEvent;
        Player.PlayerKilledEvent -= HandlePlayerKilledEvent;
        SceneManager.sceneLoaded -= HandleLevelLoadedEvent;
    }

    #region Scene management methods

    public void LoadLevel(int level) => SceneManager.LoadScene(Levels.Get(level));

    public void LoadScene(string name) => SceneManager.LoadScene(name);

    #endregion

    #region Event handlers
    private void HandleLevelLoadedEvent(Scene scene, LoadSceneMode arg1)
    {
        if (scene.name != "MainGameMenu")
        {
            levelStarted = true;
            level = scene.name;
        }
    }

    private void HandleEnemyKilledEvent(Enemy enemy)
    {
        player.Score += enemy.ScoreValue;
        if(enemy.CompareTag("Boss"))
        {
            highScore.SaveScore(player.Score);
            SceneManager.LoadScene("MainGameMenu");
        }
    }

    private void HandlePlayerKilledEvent(Player player)
    {
        highScore.SaveScore(this.player.Score);
        this.player.Score = 0;
        SceneManager.LoadScene("MainGameMenu");
    }
    #endregion
}
