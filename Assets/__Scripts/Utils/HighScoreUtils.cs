using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Class to provide High Scores functionality for the game.
/// Writing/reading high scores to storage. View high scores in-game.
/// </summary>


public class HighScoreUtils : MonoBehaviour
{


    [SerializeField]
    public List<TMPro.TMP_Text> scores;

    private List<Score> hs;

    public void WriteScores()
    {
        hs = GetHighScore();
        int i = 0;
        foreach (var n in hs)
        {
            scores[i].text = n.Name + ": " + n.ScoreValue; 
            i++;
        }
    }

    public void SaveScore(int score, string name)
    {
        string file = "hs.dat";
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        Debug.Log(docPath);
        if (!File.Exists(docPath + "/" + file))
        {
            using (var sw = File.CreateText(docPath + "/" + file))
            {
                sw.WriteLine(score + " " + name);
            }
        }
        else
        {
            using (var sw = File.AppendText(docPath + "/" + file))
            {
                sw.WriteLine(score + " " + name);
            }
        }
    }

    public List<Score> GetHighScore()
    {
        List<Score> hss = new List<Score>();
        string file = "hs.dat";
        List<Score> highScores = new List<Score>();
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
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
            highScores.Sort();
            highScores.Reverse();
            int i = 0;
            foreach (var h in highScores)
            {
                hss.Add(h);
                i++;
                if (i >= 10)
                {
                    break;
                }
            }
        }       
        return hss;
    }

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
