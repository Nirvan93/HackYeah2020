using System;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private void Awake() { Instance = this; }

    public MovementPreset Preset;

    [Header("- Refrencje -")]
    public PhysicMaterial MFriction;
    public PhysicMaterial MSlide;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        CustomMotorUpdate();
        return;
        UpdateInput();
        UpdateMotor();
    }

    private void FixedUpdate()
    {
        return;
        PhysicsCalculations();
    }

    internal Rigidbody R { get { return rigbody; } }

}
