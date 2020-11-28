using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : Singleton<LevelsManager>
{
    public void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SelectedLevel(int levelId)
    {
        //Here should be a level to level transition 
        LoadScene("Level_" + levelId);
    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
