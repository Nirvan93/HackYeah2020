using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : Singleton<LevelsManager>
{
    private int _currentLevelId = 0;

    public void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SelectedLevel(int levelId)
    {
        //Here should be a level to level transition 
        _currentLevelId = levelId;
        LoadScene("Level_" + levelId);
    }

    public void TryLoadingNextLevel()
    {
        SelectedLevel(_currentLevelId+1);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ResetLevel()
    {
        SelectedLevel(_currentLevelId);
    }

}
