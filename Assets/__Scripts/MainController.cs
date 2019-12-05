using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    // == fields ==
    [SerializeField]
    private SceneController sceneController;

    [SerializeField]
    private HighScoreUtils highScore;

    [SerializeField]
    private Player player;

    private IList<SpawnPoint> spawnPoints;

    private bool levelStarted = false;
    private float levelTimeLimit = 10;

    private void Start()
    {
        
        
    }

    private void Update()
    {
       if(levelStarted) Timer(); 
    }

    private void Timer()
    {

        if (Time.timeSinceLevelLoad > levelTimeLimit + 5)
        {
            SpawnBoss();
            levelStarted = false;
           
        }
      
    }

    private void StopBasicEnemySpawning()
    {
        spawnPoints = GameObject.Find("Spawners").GetComponentsInChildren<SpawnPoint>();
        spawnPoints.ToList().ForEach(s =>
        {
            s.CancelInvoke();
            //s.gameObject.SetActive(false);
        });

        bool allInactive = spawnPoints.All(s => s.gameObject.activeSelf == false);
        Debug.Log(allInactive);
    }

    private void SpawnBoss()
    {
        spawnPoints = GameObject.Find("Spawners").GetComponentsInChildren<SpawnPoint>();
        
        spawnPoints[0].BossSpawn();
    }

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
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(Level.Get(level));
    }
    #endregion

    #region Event handlers
    private void HandleLevelLoadedEvent(Scene scene, LoadSceneMode arg1)
    {
        if (scene.name != "MainGameMenu")
        {
            levelStarted = true;
            levelTimeLimit = Level.Levels[scene.name].LevelTime;
        }
    }

    private void HandleEnemyKilledEvent(Enemy enemy)
    {
        player.Score += enemy.ScoreValue;
        
    }

    

    private void HandlePlayerKilledEvent(Player player)
    {
        Debug.Log("This: " + this.player.Score + " Another: " + player.Score);
        highScore.SaveScore(player.Score);
        this.player.Score = 0;
        SceneManager.LoadScene("MainGameMenu");
    }
    #endregion
}
