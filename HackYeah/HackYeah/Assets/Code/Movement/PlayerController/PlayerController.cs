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
        UpdateInput();
        UpdateMotor();
    }

    private void FixedUpdate()
    {
        PhysicsCalculations();
    }

    internal Rigidbody R { get { return rigbody; } }

}
