using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperpowersManager : Singleton<SuperpowersManager>
{
    [SerializeField]
    private List<SuperPower> _allSuperPowers = new List<SuperPower>();

    [SerializeField]
    private PlayerController _playerController = null;

    private SuperPower _activeSuperPower = null;

    public void Start()
    {
        InitializeSuperpowers();   
    }


    // Update is called once per frame
    void Update()
    {
        CheckChangeSuperPowerInput();
    }

    private void InitializeSuperpowers()
    {
        foreach (SuperPower superPower in _allSuperPowers)
        {
            superPower.Initialize(_playerController);
        }
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

        if (GameUiController.Instance != null)
            GameUiController.Instance.SetSuperPowerActivated(superPowerToActivate.SuperPowerType);
    }
}
