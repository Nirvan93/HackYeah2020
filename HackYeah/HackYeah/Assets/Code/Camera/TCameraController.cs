using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCameraController : Singleton<TCameraController>
{
    public Camera CameraComponent;

    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private Rigidbody _targetRigidbody = null;

    [SerializeField]
    private float _movementSpeed = 5f;

    [SerializeField]
    private Vector3 _offsetPositon = new Vector3(0, 0, -10);

    private Vector3 _calculatedBonus;

    [SerializeField]
    private float _maximumSpeedToHandle = 50f;
    private float _minimumSpeedToHandle = 0f;

    [SerializeField]
    private Vector2 _zDistanceRange = new Vector2(10, 40);

    [SerializeField]
    private Vector2 _directionBonusRange = new Vector2(0, 10);

    // Camera shake
    private float _shakeTime = 0;
    private float _shakePower = 1f;
    private float _decreaseFactor = 1.0f;
    private Vector3 _shakeBonus;

    public void Start()
    {
        CameraComponent = GetComponent<Camera>();
    }

    // Update is called once per frame // NO CO TY
    void Update()
    {
        if (_target == null)
            return;

        //if (Input.GetKeyDown(KeyCode.E))
        //    ScreenShake(Random.Range(2f, 5f), 0.3f);

        CalculateShakeBonus();
        CalculateZDistance();
        CalculateDirectionBonus();

        transform.position = Vector3.Lerp(transform.position, _target.position + _offsetPositon + _calculatedBonus + _shakeBonus, _movementSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, _target.position + _offsetPositon  + _calculatedBonus+ _shakeBonus, _movementSpeed * Time.fixedDeltaTime);
    }

    public void SetTarget(Transform targetTransform)
    {
        _target = targetTransform;
    }

    private void CalculateShakeBonus()
    {
        if (_shakeTime > 0)
        {
            _shakeBonus = Random.insideUnitSphere * _shakePower;
            _shakeBonus = new Vector3(_shakeBonus.x, _shakeBonus.y, _shakeBonus.z);
            _shakeTime -= Time.deltaTime * _decreaseFactor;
        }
        else
        {
            _shakeBonus = Vector3.zero;
        }
    }

    private void CalculateZDistance()
    {
        if (_targetRigidbody != null)
        {
            float currentTargetSpeed = _targetRigidbody.velocity.magnitude;
            float speedFactor = Mathf.InverseLerp(_minimumSpeedToHandle, _maximumSpeedToHandle, currentTargetSpeed);
            //Debug.Log("Current speed " + currentTargetSpeed + " speed factor " + speedFactor);
            _calculatedBonus.z = Mathf.Lerp(_zDistanceRange.x, _zDistanceRange.y, speedFactor) * -1;
        }
    }

    private void CalculateDirectionBonus()
    {
        if (_targetRigidbody != null)
        {
            float currentTargetSpeed = _targetRigidbody.velocity.magnitude;
            if(currentTargetSpeed<1)
            {
                _calculatedBonus.x = 0;
                _calculatedBonus.y = 0;
                return;
            }

            float speedFactor = Mathf.InverseLerp(_minimumSpeedToHandle, _maximumSpeedToHandle, currentTargetSpeed);

            Vector3 directionBonus = _targetRigidbody.velocity.normalized * Mathf.Lerp(_directionBonusRange.x, _directionBonusRange.y, speedFactor);
            _calculatedBonus.x = directionBonus.x;
            _calculatedBonus.y = directionBonus.y;
        }
    }

    public void ScreenShake(float shakePower, float shakeTime)
    {
        _shakeTime = shakeTime;
        _shakePower = shakePower;
    }
}
