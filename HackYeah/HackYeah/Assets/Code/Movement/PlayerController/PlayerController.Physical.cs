using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    public bool IsGrounded = false;
    protected bool isJumping = false;
    protected float triggerJumping = 0f;

    protected Rigidbody rigbody;
    protected CapsuleCollider capsuleCollider;
    protected bool updateMovement = true;

    public bool SwitchOffGravity = false;

    public void SwitchBasicPhysicsLogics(bool turnOn)
    {
        updateMovement = turnOn;
    }

    private Vector3 overrideVelo = Vector3.zero;
    public void OverrideVelocity(Vector3 velo)
    {
        overrideVelo = velo;
    }


    private void PhysicsCalculations()
    {
        if (overrideVelo != Vector3.zero)
        {
            rigbody.velocity = overrideVelo;
            rigbody.useGravity = !SwitchOffGravity;
            overrideVelo = Vector3.zero;
        }
        else
        {
            Vector3 velocityMemory = rigbody.velocity;
            Vector3 targetVelo = smoothedAcceleration * Preset.MaxSpeed;

            // Gravity -----------------------
            if (!SwitchOffGravity)
            {
                if (Preset.Gravity != 1f)
                {
                    rigbody.useGravity = false;
                    rigbody.AddForce(Physics.gravity * rigbody.mass * Preset.Gravity);
                }
                else rigbody.useGravity = true;
            }
            else
                rigbody.useGravity = false;


            // Jumping -----------------------
            if (triggerJumping != 0f)
            {
                targetVelo.y = triggerJumping;
                rigbody.MovePosition(rigbody.position + transform.up * triggerJumping * 0.01f);
                OnJump();
                triggerJumping = 0f;
                IsGrounded = false;
            }
            else targetVelo.y = velocityMemory.y;

            // Apply -----------------------
            if (!IsGrounded || targetVelo.sqrMagnitude > Preset.MaxSpeed * 0.2f) capsuleCollider.material = MSlide; else capsuleCollider.material = MFriction;

            rigbody.velocity = targetVelo;
            rigbody.angularVelocity = ToAngularVelocity(currentRotation * Quaternion.Inverse(rigbody.rotation)) / Time.fixedDeltaTime;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!IsGrounded)
            if (collision != null)
                if (collision.contacts.Length > 0)
                {
                    for (int i = 0; i < collision.contacts.Length; i++)
                    {
                        float dot = Vector3.Dot(Vector3.up, collision.contacts[i].normal);
                        if (dot > 0.25f)
                        {
                            if (!IsGrounded) OnHitGround(collision.relativeVelocity);
                            return;
                        }
                    }
                }
    }

    private Vector3 ToAngularVelocity(Quaternion deltaRotation)
    {
        float angle; Vector3 axis;
        deltaRotation.ToAngleAxis(out angle, out axis);
        return axis * (angle * Mathf.Deg2Rad);
    }
}
