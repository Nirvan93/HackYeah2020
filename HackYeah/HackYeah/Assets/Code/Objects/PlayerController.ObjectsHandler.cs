using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    [Header("Pickable Objects Handling")]
    public PickableObject TestPickableObject = null;

    [SerializeField]
    private Transform _handTransform = null;

    [SerializeField]
    private float _normalThrowPower = 30f;
    [SerializeField]
    private float _superPowerThrowPower = 80f;

    private PickableObject _currentlyPickedObject = null;
    
    public void PickObject(PickableObject pickable)
    {
        if(_currentlyPickedObject!=null)
        {
            _currentlyPickedObject.Lost();
        }

        _currentlyPickedObject = pickable;
        _currentlyPickedObject.PickedUp();
        _currentlyPickedObject.transform.SetParent(_handTransform);
        _currentlyPickedObject.transform.localPosition = Vector3.zero;
    }

    public void ThrowObject()
    {
        if(_currentlyPickedObject!=null)
        {
            var worldMousePosition = Input.mousePosition;
            worldMousePosition.z = (transform.position.z - TCameraController.Instance.transform.position.z);
            worldMousePosition = TCameraController.Instance.CameraComponent.ScreenToWorldPoint(worldMousePosition);
            worldMousePosition.z = transform.position.z;

            Vector3 throwDirection = (worldMousePosition - transform.position).normalized;

            _currentlyPickedObject.transform.SetParent(null);
            _currentlyPickedObject.Thrown(throwDirection * (SuperStrengthPower.SuperStrengthPowerActive ? _superPowerThrowPower : _normalThrowPower));
            _currentlyPickedObject = null;
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
