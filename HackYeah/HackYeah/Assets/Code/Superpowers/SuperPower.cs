using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class SuperPower : MonoBehaviour
{
    public ESuperPowerType SuperPowerType;

    public KeyCode KeyCodeToActivate = KeyCode.Alpha0;

    public bool IsAvailable = false;

    public virtual void OnActivate()
    {
        Debug.Log("Activated superpower " + SuperPowerType.ToString());
    }

    public virtual void OnDeactivate()
    {
        Debug.Log("Dectivated superpower" + SuperPowerType.ToString());
    }
}
