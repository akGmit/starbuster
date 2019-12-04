using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // == fields ==
    [SerializeField]
    private SceneControler sceneControler;

    [SerializeField]
    private HighScoreUtils highScore;

    [SerializeField]
    private Player player;

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

        if (player.Score > 100)
        {
            StopBasicEnemySpawning();
        }
    }

    private void StopBasicEnemySpawning()
    {
        spawnPoints = GameObject.Find("Spawners").GetComponentsInChildren<SpawnPoint>();

        spawnPoints.ToList().ForEach(s =>
        {
            s.CancelInvoke();
            s.gameObject.SetActive(false);
        });

        bool allInactive = spawnPoints.All(s => s.gameObject.activeSelf == false);
        Debug.Log(allInactive);
    }

    private void HandlePlayerKilledEvent(Player player)
    {
        Debug.Log("This: " + this.player.Score + " Another: " + player.Score);
        highScore.SaveScore(player.Score);
        this.player.Score = 0;
        sceneControler.LoadMainMenu();
    }
}
