﻿using System;
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
        InitRagdoll();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (updateMovement)
        {
            UpdateInput();
            UpdateMotor();
        }

        UpdateGlowEffect();
        UpdateAnimating();
    }

    private void FixedUpdate()
    {
        if ( updateMovement) PhysicsCalculations();
    }

    internal Rigidbody R { get { return rigbody; } }

}
