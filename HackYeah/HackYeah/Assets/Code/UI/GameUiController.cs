using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiController : Singleton<GameUiController>
{
    [SerializeField]
    private List<UiSuperPower> _superPowerIndicators = new List<UiSuperPower>();

    [SerializeField]
    private FadingUI _fadingUI = null;

    public void Start()
    {
        foreach (UiSuperPower superPower in _superPowerIndicators)
        {
            superPower.SetActivatedValue(false);
            superPower.SetAvailable(SuperpowersManager.Instance.IsSuperPowerAvailable(superPower.SuperPowerType));
        }
    }


    public void SetSuperPowerActivated(ESuperPowerType superPowerType)
    {
        foreach(UiSuperPower superPower in _superPowerIndicators)
        {
            superPower.SetActivatedValue(superPowerType == superPower.SuperPowerType);
        }
    }
    

    public void ShowFadingUI()
    {
        StartCoroutine(WaitFewSecondsAndShowFadingUI());
    }

    private IEnumerator WaitFewSecondsAndShowFadingUI()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(_fadingUI.DeathUI());

    }
}
