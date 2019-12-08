using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class to hold individual game level parameters.
/// </summary>
static class Levels
{
    public const string One = "Level1";
    public const string Two = "Level2";
    public const string Three = "Level3";
    public const string Four = "Level4";
    public const string Five = "Level5";

    public static LevelSettings Level1 = new LevelSettings("Level1",
        0.05f, 10, 10, 5, 3.5f, 1, 1, 4, 3, 10f);

    public static LevelSettings Level2 = new LevelSettings("Level2",
        0.05f, 10, 10, 5, 3.5f, 1, 1, 4, 3, 20f);

    public static Dictionary<string, LevelSettings> Level = new Dictionary<string, LevelSettings>()
    {
        {"Level1", Level1},
        {"Level2", Level2}
    };
    
    
    /// <summary>
    /// Get string representation of level name.
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public static string Get(int level)
    {
        string lvl;
        switch (level)
        {
            case 1:
                lvl = One;
                break;
            case 2:
                lvl = Two;
                break;
            case 3:
                lvl = Three;
                break;
            case 4:
                lvl = Four;
                break;
            case 5:
                lvl = Five;
                break;
            default:
                throw new Exception("Level name not recognized");
        }
        return lvl;
    }
    
    
    //public static LevelSettings GetLevelSettings(int level)
    //{
    //    LevelSettings lvl;
    //    switch (level)
    //    {
    //        case 1:
    //            lvl = Level1;
    //            break;
    //        case 2:
    //            lvl = Level2;
    //            break;
    //        //case 3:
    //        //    lvl = Three;
    //        //    break;
    //        //case 4:
    //        //    lvl = Four;
    //        //    break;
    //        //case 5:
    //        //    lvl = Five;
    //        //    break;
    //        default:
    //            throw new Exception("Level name not recognized");
    //    }
    //    return lvl;
    //}

}
