using UnityEngine;

public class motor3 : MonoBehaviour
{
    public Transform targetPoint1;
    public Transform targetPoint2;
    public Transform targetPoint3;
    
    [Space(5)]
    public float maxPower = 2f;
    public float accelerationSensitivity = 5f;

    [Space(5)]
    public float decelerationDistance = 1f;

    private Transform target;
    private float distance;

    private Vector3 output;
    private Vector3 acceleration;
    private Vector3 targetAcceleration;
    private float motorPower = 0f;

    void Start()
    {
        target = targetPoint1;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) target = targetPoint1;
        if (Input.GetKey(KeyCode.Alpha2)) target = targetPoint2;
        if (Input.GetKey(KeyCode.Alpha3)) target = targetPoint3;

        distance = Vector3.Distance(transform.position, target.position);
        Vector3 moveDir = (target.position - transform.position).normalized;

        if (distance > decelerationDistance)
        {
            targetAcceleration = Vector3.LerpUnclamped(Vector3.zero, moveDir, motorPower);
            targetAcceleration *= .1f + Mathf.InverseLerp(0f, decelerationDistance, distance) * 0.9f;

            motorPower += Time.deltaTime * accelerationSensitivity;
            if (motorPower > maxPower) motorPower = maxPower;
        }
        else
        {
            targetAcceleration = Vector3.zero;
            motorPower -= Time.deltaTime * accelerationSensitivity;
            if (motorPower < 0f) motorPower = 0f;
        }

        acceleration = Vector3.Lerp(acceleration, targetAcceleration, Time.deltaTime * accelerationSensitivity);
        output = Vector3.Lerp(output, acceleration, Time.deltaTime * 7f);
        transform.position += output * Time.deltaTime;
    }
}
