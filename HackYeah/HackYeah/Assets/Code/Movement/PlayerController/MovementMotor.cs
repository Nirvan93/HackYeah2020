using UnityEngine;

[System.Serializable]
public class MovementMotor
{
    public float maxPower = 500f;
    public float accelerationSensitivity = 150f;
    public float decelerationDistance = 1f;
    public float lerpSpd = 10f;
    public Vector3 targetPos;
    public Vector3 Output { get; private set; }

    private float distance;
    private Vector3 acceleration;
    private Vector3 targetAcceleration;
    private float motorPower = 0f;


    public void Reset()
    {
        distance = 0f;
        acceleration = Vector3.zero;
        targetAcceleration = Vector3.zero;
        motorPower = 0f;
        Output = Vector3.zero;
    }

    public void SlowDown(float power)
    {
        motorPower = Mathf.Lerp(motorPower, 0f, Time.deltaTime * power);
        targetAcceleration = Vector3.Lerp(targetAcceleration, Vector3.zero, Time.deltaTime * power);
    }


    public void RushAcceleration(float amount)
    {
        motorPower += amount;
    }

    public void Update(Vector3 currentPos)
    {

        if (targetPos == Vector3.zero)
        {
            distance = 0f;
        }
        else
            distance = Vector3.Distance(currentPos, targetPos);

        if (distance > decelerationDistance)
        {
            Vector3 moveDir = (targetPos - currentPos).normalized;

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
        Output = Vector3.Lerp(Output, acceleration, Time.deltaTime * lerpSpd);
    }
}
