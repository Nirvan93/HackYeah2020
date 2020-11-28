using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpeedPower : SuperPower
{
    public static bool SuperSpeedActivated = false;

    public void Start()
    {
        SuperSpeedActivated = false;
    }

    public override void OnActivate()
    {
        base.OnActivate();
        SuperSpeedActivated = true;
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();
        SuperSpeedActivated = false;
    }
}
