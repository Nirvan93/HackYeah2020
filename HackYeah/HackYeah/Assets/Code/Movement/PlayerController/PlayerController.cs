using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
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
        UpdateInput();
        UpdateMotor();
    }

    private void FixedUpdate()
    {
        PhysicsCalculations();
    }
}
