using System.Collections;
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
    private Quaternion leftRot;
    private Quaternion rightRot;

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
        leftRot = Quaternion.Euler(0, -90, 0);
        rightRot = Quaternion.Euler(0, -270, 0);
    }

    void Update()
    {

        


        switch (aiState)
        {
            case AIState.Flying:

               // float ySpeed = 0.2f + Mathf.PerlinNoise(112f, Time.time) * 0.3f;
             //   targetVelocity = new Vector3(directionValue, ySpeed, 0);

                if(transform.position.y>FlyLimit.y)
                {
                    aiState = AIState.Landing;
                }

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
                
                targetVelocity.y = 1;


                if (elapsed > 100f)
                {
                    aiState = AIState.Landing;
                    elapsed = 0f;
                }

                break;

            case AIState.Landing:

                GroundDetection();
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
        if (movingRight)
            rigbody.MoveRotation(rightRot);
        else
            rigbody.MoveRotation(leftRot);

        rigbody.velocity = targetVelocity * Speed;
    }


    public void GroundDetection()
    {
        if (Physics.Raycast(transform.position, Vector3.down * 1.5f, GroundedDistance, groundMask))
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
    }

    private void OnDrawGizmosSelected()
    {
        // if (Application.isPlaying) return;
        Gizmos.DrawSphere(transform.position + LeftBorder, 0.1f);
        Gizmos.DrawSphere(transform.position + RightBorder, 0.1f);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, FlyLimit.y, 0));
        Gizmos.DrawRay(transform.position, Vector3.down * 1.5f * GroundedDistance);
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
