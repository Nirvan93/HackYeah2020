using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementPreset-", menuName = "HackYeah/MovementPreset", order = 1)]
public class MovementPreset : ScriptableObject
{
    public float MaxSpeed = 10f;
    public float AccelerationDuration = 0.2f;
    public float DecelerationDuration = 0.2f;
    public float RotationSpeed = 0.25f;

    public float Gravity = 1f;

}