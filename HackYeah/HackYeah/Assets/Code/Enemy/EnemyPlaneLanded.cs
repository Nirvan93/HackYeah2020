﻿using System.Collections;
using UnityEngine;

public class EnemyPlaneLanded : MonoBehaviour
{
    public float Speed = 1f;
    public Vector3 LeftBorder = Vector3.left;
    public Vector3 RightBorder = Vector3.right;
    public Vector3 FlyLimit = Vector3.zero;
    public float GroundedDistance = 1f;

    private bool movingRight = true;
    private Vector3 startPosition;
    private float elapsed = 0f;
    private bool isGrounded = false;
    private int groundMask;
    private bool isCoroutineWorking = false;
    private int directionValue;


    protected Rigidbody rigbody;

    public enum AIState { Flying, Landing, Standing }
    public AIState aiState = AIState.Flying;

    private void Awake()
    {
        rigbody = GetComponent<Rigidbody>();
        groundMask = LayerMask.GetMask("Ground");
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {


        GroundDetection();

        if (rigbody.position.x <= startPosition.x + LeftBorder.x)
        {
            movingRight = true;

        }

        if (rigbody.position.x > startPosition.x + RightBorder.x)
        {
            movingRight = false;
        }

        if (movingRight)
            directionValue = 1;
        else
            directionValue = -1;

        switch (aiState)
        {
            case AIState.Flying:

                if (transform.position.y > FlyLimit.y)
                {
                    aiState = AIState.Landing;
                }

                targetVelocity.y = 1;


                if (elapsed > 10f)
                {
                    Debug.Log("Elapsed " + elapsed);
                    aiState = AIState.Landing;
                    elapsed = 0f;
                }

                break;

            case AIState.Landing:

                targetVelocity = new Vector3(directionValue, -1f, 0);
                if (isGrounded)
                {
                    aiState = AIState.Standing;
                    Debug.Log("Błąd");
                }
                break;

            case AIState.Standing:
                StartCoroutine(IStanding());
                break;
        }

        elapsed += 0.1f;
    }

    private void FixedUpdate()
    {
        if (isCoroutineWorking) return;

        Movement();
    }

    Vector3 targetVelocity;
    public void Movement()
    {
        rigbody.velocity = targetVelocity * Speed;
    }


    public void GroundDetection()
    {
        if (Physics.Raycast(transform.position, Vector3.down, GroundedDistance, groundMask))
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying) return;
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawSphere(transform.position + LeftBorder, 0.5f);
        Gizmos.DrawSphere(transform.position + RightBorder, 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, FlyLimit.y, 0));
        Gizmos.DrawRay(transform.position, Vector3.down * GroundedDistance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].otherCollider.gameObject.tag == "Player")
        {
            float dot =
            Vector3.Dot(collision.contacts[0].normal, Vector3.up);

            if (targetVelocity.y < 0f)
                if (dot > 0.5f)
                    PlayerController.Instance.AddForceToRagdollBodies(targetVelocity * 100f);
            //if (collision.contacts[0].otherCollider.attachedRigidbody)
            //{
            //    collision.contacts[0].otherCollider.attachedRigidbody.AddForce(targetVelocity*10f, ForceMode.Impulse);
            //}
        }
    }

    public IEnumerator IStanding()
    {
        isCoroutineWorking = true;
        yield return new WaitForEndOfFrame();

        if (isGrounded)
        {
            yield return new WaitForSeconds(.5f);
        }

        aiState = AIState.Flying;
        isCoroutineWorking = false;
        elapsed = 0f;
        yield break;
    }
}
