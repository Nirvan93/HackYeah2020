using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPower : SuperPower
{

    [SerializeField]
    private Flying _flyingController;

    public override void OnActivate()
    {
        base.OnActivate();
        _flyingController.EnableFly(true);
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();
        _flyingController.EnableFly(false);
    }
}
