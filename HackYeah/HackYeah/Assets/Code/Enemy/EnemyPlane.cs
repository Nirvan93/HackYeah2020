using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    public Vector3 Left = Vector3.left;
    public Vector3 Right = Vector3.right;
    public float Speed = 1;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Quaternion leftRotation;
    private Quaternion rightRotation;
    private bool toTheRight = true;
    private float progress = 0f;

    protected Rigidbody rigbody;

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying) return;
        Gizmos.DrawSphere(transform.position + Left, 0.1f);
        Gizmos.DrawSphere(transform.position + Right, 0.1f);
    }

    private void Awake()
    {
        rigbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        startPosition = transform.position + Left;
        targetPosition = transform.position + Right;
        leftRotation = Quaternion.Euler(0, -90, 0);
        rightRotation = Quaternion.Euler(0, 90, 0);
    }

    void FixedUpdate()
    {
        if (progress >= 1) toTheRight = false;
        if (progress <= 0) toTheRight = true;

        if (toTheRight == true)
        {
            progress += Time.deltaTime * Speed;
            rigbody.MoveRotation(rightRotation);
        }
        else
        {
            progress -= Time.deltaTime * Speed;
            rigbody.MoveRotation(leftRotation);
        }

        rigbody.MovePosition(Vector3.Lerp(startPosition, targetPosition, progress));
    }
}
