using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

[Serializable]
public class HighScoreUtils : MonoBehaviour
{

    [SerializeField]
    private List<int> hs;

    [SerializeField]
    public SortedSet<string> bb;

    [SerializeField]
    public int bbb;

    [SerializeField]
    public Button scores;

    public void Start()
    {
        hs = GetHighScore();

        foreach( int n in hs)
        {
           
  
            scores.transform.position = new Vector2(30.0f + 0.0f, 50.0f + 100.05f);
            
            Instantiate(scores);
            Destroy(scores);

            
        }
        

    }

    public void SaveScore(int score)
    {
        string file = "hs.dat";
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        if (!File.Exists(docPath + "/" + file))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(docPath + "/" + file))
            {
                sw.WriteLine(score);
            }
        }
        else {

            using (StreamWriter sw = File.AppendText(docPath + "/" + file))
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
            using (StreamReader sr = File.OpenText(docPath + "/" + file))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    highScores.Add(int.Parse(s));
                }
            }
        }
        bbb = highScores.Count;
        //bb = highScores;
        //highScores.Add("8000");
        //highScores.Add("3333");
        var rev = highScores.Reverse();
        List<int> hss = new List<int>();
        int i = 0;
        foreach(var h in rev)
        {
            hss.Add(h);
            i++;
            if (i >= 9)
                break;
        }

        
        return hss;

    }
}
