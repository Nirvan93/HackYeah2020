using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDataManager
{
    public static int CurrentLevel=0;

    private const string CURRENT_LEVEL_KEY = "curLev";
    
    public static void SaveCurrentLevel()
    {
        PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, CurrentLevel);
    }

    public static void LoadCurrentLevel()
    {
        CurrentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL_KEY, 0);
    }
}
