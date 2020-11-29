using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    public bool IsGrounded = false;
    protected bool isJumping = false;
    protected float triggerJumping = 0f;

    protected Rigidbody rigbody;
    internal CapsuleCollider capsuleCollider;
    protected bool updateMovement = true;

    public bool SwitchOffGravity = false;
    private Vector3 previousVelo;
    private Vector3 previousVelo2;

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

            if (IsGrounded)
            {
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
                rigbody.angularVelocity = ToAngularVelocity(currentRotation * Quaternion.Inverse(rigbody.rotation)) / (Time.fixedDeltaTime * 3f);
            }
            else
            {
                rigbody.AddForce(targetVelo * 3f, ForceMode.Acceleration);
            }

        }

        previousVelo2 = previousVelo;
        previousVelo = rigbody.velocity;
    }

    public float KillVelocity = 100f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
            if (collision.contacts.Length > 0)
            {
                if (collision.relativeVelocity.magnitude > 1f)
                    PlayerAudio.Instance.PlayAudio(PlayerAudio.Instance.Steps);

                float relDot = Vector3.Dot(collision.relativeVelocity.normalized, collision.contacts[0].normal);
                //Debug.Log("rel dot = " + relDot);
                //Debug.Log("rel velo = " + collision.relativeVelocity + " magn = " + collision.relativeVelocity.magnitude);

                if (relDot > 0.3f && relDot < 0.7f)
                {
                }
                else
                {
                    if (collision.relativeVelocity.magnitude > KillVelocity)
                    {
                        TCameraController.Instance.ScreenShake(1f + collision.relativeVelocity.magnitude / 14f, 0.1f + collision.relativeVelocity.magnitude / 250f);
                        SwitchRagdoll(true, true);

                        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Bullets"))
                            AddForceToRagdollBodies(collision.relativeVelocity * 0.75f);
                        else
                            AddForceToRagdollBodies(-collision.relativeVelocity * 2f);
                    }
                }

                if (!IsGrounded)
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

    public static Vector3 ToAngularVelocity(Quaternion deltaRotation)
    {
        float angle; Vector3 axis;
        deltaRotation.ToAngleAxis(out angle, out axis);
        return axis * (angle * Mathf.Deg2Rad);
    }

    public Vector3 GetVelocity()
    {
        return rigbody.velocity;
    }
}
