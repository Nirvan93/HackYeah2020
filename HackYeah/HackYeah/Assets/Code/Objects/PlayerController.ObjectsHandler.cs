using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    [Header("Pickable Objects Handling")]
    [SerializeField]
    private Transform _handTransform = null;

    [SerializeField]
    private float _normalThrowPower = 30f;

    private PickableObject _currentlyPickedObject = null;

    [SerializeField]
    private float _superPowerThrowMultiplier = 20f;
    private bool _throwKeyPressed = false;
    private float _throwKeyPressedTime = 0;

    public void PickObject(PickableObject pickable)
    {
        if(_currentlyPickedObject!=null)
        {
            _currentlyPickedObject.Lost();
        }

        _currentlyPickedObject = pickable;
        _currentlyPickedObject.PickedUp();
        _currentlyPickedObject.transform.SetParent(transform);
        _currentlyPickedObject.transform.position = _handTransform.position;
    }

    public void ThrowObject(float throwPower)
    {
        if(_currentlyPickedObject!=null)
        {
            var worldMousePosition = Input.mousePosition;
            worldMousePosition.z = (transform.position.z - TCameraController.Instance.transform.position.z);
            worldMousePosition = TCameraController.Instance.CameraComponent.ScreenToWorldPoint(worldMousePosition);
            worldMousePosition.z = transform.position.z;

            Vector3 throwDirection = (worldMousePosition - transform.position).normalized;

            _currentlyPickedObject.transform.SetParent(null);
            _currentlyPickedObject.Thrown(throwDirection * throwPower);
            _currentlyPickedObject = null;
        }
    }

    public void ProcessThrowingInput()
    {
        if(!SuperStrengthPower.SuperStrengthPowerActive)
        {
            if (Input.GetMouseButtonDown(1))
                ThrowObject(_normalThrowPower);
        }
        else
        {
            ProcessSuperStrengthInput();
        }
    }

    private void ProcessSuperStrengthInput()
    {
        if (_throwKeyPressed)
        {
            _throwKeyPressedTime += Time.deltaTime;
            if (Input.GetMouseButtonUp(1))
            {
                ThrowObject(_throwKeyPressedTime * _superPowerThrowMultiplier);
               //Urwij tu łapę

                _throwKeyPressed = false;
                _throwKeyPressedTime = 0;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            _throwKeyPressed = true;
        }

    }

    public void PlayerWantsToPickAnyObject()
    {
        PickableObject anyPickableInRange = SearchForPickablesNearPlayer();
        if (anyPickableInRange != null)
            PickObject(anyPickableInRange);
    }

    public PickableObject SearchForPickablesNearPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.attachedRigidbody == null)
                continue;

            PickableObject pickable = hitCollider.attachedRigidbody.GetComponent<PickableObject>();
            return pickable;
        }

        return null;
    }
}
