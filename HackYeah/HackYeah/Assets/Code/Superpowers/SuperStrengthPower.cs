using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperStrengthPower : SuperPower
{
    public static bool SuperStrengthPowerActive = false;
    public override void OnActivate()
    {
        base.OnActivate();
        SuperStrengthPowerActive = true;
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();
        SuperStrengthPowerActive = false;
    }
}
