using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(string level) => SceneManager.LoadScene(level);

    public void LoadScene(string name) => SceneManager.LoadScene(name);
}
