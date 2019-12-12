using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinished : MonoBehaviour
{
    [SerializeField]
    private GameObject levelFinishedMenu;

    [SerializeField]
    private TMPro.TMP_Text finalScore;

    [SerializeField]
    private TMPro.TMP_InputField nameField;

    [SerializeField]
    private HighScoreUtils highScore;

    public void LevelFinishedMenu(int score)
    {
        Time.timeScale = 0;
        levelFinishedMenu.SetActive(true);
        finalScore.text = score + "";
    }

    public void SaveScore()
    {
        if (highScore.CheckIfInTopTen(int.Parse(finalScore.text))){

            highScore.SaveScore(int.Parse(finalScore.text), nameField.text);
        }
        SceneManager.LoadScene("MainGameMenu");
    }
}
