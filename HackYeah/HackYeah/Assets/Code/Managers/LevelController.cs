using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    [SerializeField]
    private List<ESuperPowerType> _availableSuperPowers = new List<ESuperPowerType>();

    public void FinishLevel()
    {
        LevelsManager.Instance.TryLoadingNextLevel();
    }

    public void Start()
    {
        SetupAvailableSuperPowers();
    }

    private void SetupAvailableSuperPowers()
    {
        SuperpowersManager.Instance.SetAvailableSuperPowers(_availableSuperPowers);
    }
}
