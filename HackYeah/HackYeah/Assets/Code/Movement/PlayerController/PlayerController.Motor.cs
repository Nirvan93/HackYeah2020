using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    protected Vector3 smoothedAcceleration = Vector3.zero;
    protected Vector3 veloHelper = Vector3.zero;

    private Quaternion currentRotation;
    private Quaternion targetedRotation;

    private readonly Quaternion TOLEFT = Quaternion.Euler(0f, -90f, 0f);
    private readonly Quaternion TORIGHT = Quaternion.Euler(0f, 90f, 0f);

    public void UpdateMotor()
    {
        if ( Input_Accelerate != 0f)
        {
            if (Input_Accelerate < 0f) targetedRotation = TOLEFT; else targetedRotation = TORIGHT;
            smoothedAcceleration = Vector3.SmoothDamp(smoothedAcceleration, Vector3.right * Input_Accelerate, ref veloHelper, Preset.AccelerationDuration, Mathf.Infinity, Time.deltaTime);
        }
        else
        {
            smoothedAcceleration = Vector3.SmoothDamp(smoothedAcceleration, Vector3.zero, ref veloHelper, Preset.DecelerationDuration, Mathf.Infinity, Time.deltaTime);
        }

        currentRotation = Quaternion.Lerp(currentRotation, targetedRotation, Time.deltaTime * Preset.RotationSpeed * 60f);
    }

}
