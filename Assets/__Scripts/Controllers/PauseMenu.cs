using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject shield;

    [SerializeField]
    private GameObject beam;

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void HandlePowerUpCollectedEvent(string p)
    {
        if (p == "CollectableShield")
        {
            shield.SetActive(true);
        }
        else if (p == "CollectableBeam")
        {
            beam.SetActive(true);
        }
    }

    private void OnEnable()
    {
        PowerUp.PowerUpCollectedEvent += HandlePowerUpCollectedEvent;
    }

    private void OnDisable()
    {
        PowerUp.PowerUpCollectedEvent -= HandlePowerUpCollectedEvent;
    }
}
