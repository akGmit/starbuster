using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Class to provide High Scores functionality for the game.
/// Writing/reading high scores to storage. View high scores in-game.
/// Only top 10 high scores will be stored and available for reading.
/// High scores are stored in text format file at local app data folder.
/// </summary>
public class HighScoreUtils : MonoBehaviour
{
    //Text values of High Scores UI view
    [SerializeField]
    public List<TMPro.TMP_Text> scores;

    private readonly string file = "hs.dat";
    private readonly string docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    public void WriteScores()
    {
        int i = 0;
        foreach (var n in GetHighScore())
        {
            scores[i].text = n.Name + ": " + n.ScoreValue; 
            i++;
        }
    }

    /// <summary>
    /// Get current high score list, add new score and sort list again.
    /// Overwrite existing file with new high score added, only top 10 scores are saved.
    /// </summary>
    /// <param name="score">Score to save</param>
    /// <param name="name">Name of a player</param>
    public void SaveScore(int score, string name)
    {
        //Adding to, sorting (ascending) and reversing high scores list
        List<Score> highScore = GetHighScore();
        highScore.Add(new Score(score, name));
        highScore.Sort();
        highScore.Reverse();

        using (var sw = File.CreateText(docPath + "/" + file))
        {
            foreach (Score s in highScore.GetRange(0, highScore.Count))
            {
                sw.WriteLine(s.ScoreValue + " " + s.Name);
            }
        }
    }

    /// <summary>
    /// Reads high score file from local data storage.
    /// </summary>
    /// <returns>List of sorted high scores.</returns>
    public List<Score> GetHighScore()
    {  
        List<Score> highScores = new List<Score>();

        if (File.Exists(docPath + "/" + file))
        {
            using (var sr = File.OpenText(docPath + "/" + file))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] scores = s.Split(' ');
                    highScores.Add(new Score(int.Parse(scores[0]), scores[1]));
                }
            }
            //Sorting (ascending) and reversing high scores list
            highScores.Sort();
            highScores.Reverse();
        }       
        int range = highScores.Count < 10 ? highScores.Count : 10;
        return highScores.GetRange(0, range);
    }

    /// <summary>
    /// A helper method to check if players score is in top 10.
    /// </summary>
    /// <param name="score">Score to check</param>
    /// <returns>True if score is in top10; False otherwise.</returns>
    public bool CheckIfInTopTen(int score)
    {
        var top10 = GetHighScore();
        if (top10.Count == 0)
        {
            return true;
        }else if(score >= top10.Last().ScoreValue)
        {
            return true;
        }else if(top10.Count < 10)
        {
            return true;
        }

        return false;
    }
}
