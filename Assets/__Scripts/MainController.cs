using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class MainController : MonoBehaviour
{
    #region Serialized fields

  
    
    #endregion

    private LevelSettings settings;
    private IList<SpawnPoint> spawnPoints;
    public bool levelInProgress = false;
   
    private void InitLevelFinished()
    {

    }

    private void OnEnable()
    {
        //Enemy.EnemyKilledEvent += HandleEnemyKilledEvent;
        //Player.PlayerKilledEvent += HandlePlayerKilledEvent;
       // LevelController.LevelFinshedEvent += HandleLevelFinshedEvent;
    }

   

    private void OnDisable()
    {
        //Enemy.EnemyKilledEvent -= HandleEnemyKilledEvent;
        //Player.PlayerKilledEvent -= HandlePlayerKilledEvent;
    }

    #region Scene management methods

    public void LoadLevel(string level) => SceneManager.LoadScene(level);

    public void LoadScene(string name) => SceneManager.LoadScene(name);

    #endregion
}
