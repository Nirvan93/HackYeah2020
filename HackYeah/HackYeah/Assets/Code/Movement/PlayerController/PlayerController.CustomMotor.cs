using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    public MovementMotor Motor;
    private Vector3 proceduralPosition;
    public void CustomMotorUpdate()
    {
        Motor.Update(transform.position);
        transform.position = Motor.Output;
        //proceduralPosition = Motor.Output;
    }
}
