using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    public MovementMotor Motor; 
    
    public void CustomMotorUpdate()
    {
        Motor.Update(transform);
        //transform.position += Motor.Output * Time.deltaTime;
    }
}
