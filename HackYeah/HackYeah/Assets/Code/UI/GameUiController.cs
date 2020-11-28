using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiController : Singleton<GameUiController>
{
    [SerializeField]
    private List<UiSuperPower> _superPowerIndicators = new List<UiSuperPower>();

    public void Start()
    {
        foreach (UiSuperPower superPower in _superPowerIndicators)
        {
            superPower.SetActivatedValue(false);
        }
    }


    public void SetSuperPowerActivated(ESuperPowerType superPowerType)
    {
        foreach(UiSuperPower superPower in _superPowerIndicators)
        {
            superPower.SetActivatedValue(superPowerType == superPower.SuperPowerType);
        }
    }
}
