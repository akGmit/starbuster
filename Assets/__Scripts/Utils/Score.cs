using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : IComparable<Score>
{
    private int scoreValue = 0;
    private string name = "default";

    public int ScoreValue { get => scoreValue; set => scoreValue = value; }
    public string Name { get => name; set => name = value; }

    public Score(int score, string name)
    {
        ScoreValue = score;
        Name = name;
    }

    public int CompareTo(Score other)
    {
        if(ScoreValue.CompareTo(other.ScoreValue) != 0)
        {
            return ScoreValue.CompareTo(other.ScoreValue);
        }

        return 0;
    }
}
