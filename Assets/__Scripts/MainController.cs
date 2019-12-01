using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // == fields ==
    [SerializeField]
    SceneControler sceneControler;

    
    HighScoreUtils highgScore;

    private int playerScore = 0;

    // == methods ==

    // add a method to deal with the enemy event
    // do that in the enable method

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
        playerScore += enemy.ScoreValue;
    }

    private void HandlePlayerKilledEvent(Player player)
    {
        Debug.Log(playerScore);
        highgScore.SaveScore(playerScore);
        sceneControler.LoadMainMenu();
    }

}
