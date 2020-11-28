using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperpowersManager : MonoBehaviour
{
    [SerializeField]
    private List<SuperPower> _allSuperPowers = new List<SuperPower>();

    private SuperPower _activeSuperPower = null;

    // Update is called once per frame
    void Update()
    {
        CheckChangeSuperPowerInput();
    }

    public void SetSuperPowerAvailable(ESuperPowerType superPowerType)
    {
        foreach (SuperPower superPower in _allSuperPowers)
        {
            if (superPower.SuperPowerType == superPowerType)
                superPower.IsAvailable = true;
        }
    }

    private void CheckChangeSuperPowerInput()
    {
        foreach(SuperPower superPower in _allSuperPowers)
        {
            if(superPower.IsAvailable && Input.GetKeyDown(superPower.KeyCodeToActivate))
            {
                SetCurrentSuperPower(superPower);
                break;
            }
        }
    }

    private void SetCurrentSuperPower(SuperPower superPowerToActivate)
    {
        if (_activeSuperPower != null)
            _activeSuperPower.OnDeactivate();

        _activeSuperPower = superPowerToActivate;
        _activeSuperPower.OnActivate();
    }
}
