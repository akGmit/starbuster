using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // == fields ==
    [SerializeField]
    SceneControler sceneControler;

    [SerializeField]
    HighScoreUtils highScore;

    [SerializeField]
    Player player;

    private IList<SpawnPoint> spawnPoints;


    private void OnEnable()
    { 
        // subscribe to the enemy getting killed event
        Enemy.EnemyKilledEvent += HandleEnemyKilledEvent;
        Player.PlayerKilledEvent += HandlePlayerKilledEvent;
    }

    private void OnDisable()
    {
        // unsubscribe
        Enemy.EnemyKilledEvent -= HandleEnemyKilledEvent;
        Player.PlayerKilledEvent -= HandlePlayerKilledEvent;
    }

    private void HandleEnemyKilledEvent(Enemy enemy)
    {
        player.Score += enemy.ScoreValue;

        if(player.Score > 100)
        {
            Debug.Log(player.Score);
            GameObject spawner = GameObject.Find("Spawners");
            spawner.SetActive(false);
            Debug.Log(spawner.activeSelf);
        }
    }

    private void HandlePlayerKilledEvent(Player player)
    {
        Debug.Log("This: " + this.player.Score + " Another: " + player.Score);
        highScore.SaveScore(player.Score);
        this.player.Score = 0;
        sceneControler.LoadMainMenu();
    }
}
