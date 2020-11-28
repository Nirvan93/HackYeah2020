using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class SuperPower : MonoBehaviour
{
    public ESuperPowerType SuperPowerType;

    public KeyCode KeyCodeToActivate = KeyCode.Alpha0;

    public bool IsAvailable = false;

    protected PlayerController _playerController;

    protected bool _isActive = false;

    public void Initialize(PlayerController player)
    {
        _playerController = player;
    }

    public virtual void OnActivate()
    {
        Debug.Log("Activated superpower " + SuperPowerType.ToString());
        _isActive = true;
    }

    public virtual void OnDeactivate()
    {
        Debug.Log("Dectivated superpower" + SuperPowerType.ToString());
        _isActive = false;
    }
}
