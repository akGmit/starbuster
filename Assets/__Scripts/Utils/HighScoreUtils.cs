﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Class to provide High Scores functionality for the game.
/// Writing/reading high scores to storage. View high scores in-game.
/// </summary>


public class HighScoreUtils : MonoBehaviour
{

    [SerializeField]
    private List<int> hs;

    
    public SortedSet<string> bb;


    public int bbb;

    [SerializeField]
    public List<TMPro.TMP_Text> scores;

    public void Start()
    {
        hs = GetHighScore();
        WriteScores();
    }

    public void WriteScores()
    {
        int i = 0;
        foreach (int n in hs)
        {
            scores[i].text = "Score: " + n;
            i++;
        }
    }

    public void SaveScore(int score)
    {
        string file = "hs.dat";
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        if (!File.Exists(docPath + "/" + file))
        {
            // Create a file to write to.
            using (var sw = File.CreateText(docPath + "/" + file))
            {
                sw.WriteLine(score);
            }
        }
        else
        {
            using (var sw = File.AppendText(docPath + "/" + file))
            {
                sw.WriteLine(score);
            }
        }
    }

    public List<int> GetHighScore()
    {

        string file = "hs.dat";
        SortedSet<int> highScores = new SortedSet<int>();
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        if (File.Exists(docPath + "/" + file))
        {
            using (var sr = File.OpenText(docPath + "/" + file))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    highScores.Add(int.Parse(s));
                }
            }
        }
        bbb = highScores.Count;

        var rev = highScores.Reverse();
        List<int> hss = new List<int>();
        int i = 0;
        foreach (int h in rev)
        {
            hss.Add(h);
            i++;
            if (i >= 10)
            {
                break;
            }
        }
        return hss;
    }
}