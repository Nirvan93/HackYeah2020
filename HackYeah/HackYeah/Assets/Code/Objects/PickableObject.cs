using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _objectRigidbody = null;

    private List<Collider> _objectColliders = new List<Collider>();

    public void Start()
    {
        foreach(Collider coll in GetComponentsInChildren<Collider>())
        {
            _objectColliders.Add(coll);
        }
    }


    public void PickedUp()
    {
        _objectRigidbody.isKinematic = true;
        _objectRigidbody.useGravity = false;
        SwitchColliders(false);
    }

    public void Lost()
    {
        _objectRigidbody.isKinematic = false;
        _objectRigidbody.useGravity = true;
        SwitchColliders(true);

    }

    public void Thrown(Vector3 throwVelocity)
    {
        _objectRigidbody.isKinematic = false;
        _objectRigidbody.useGravity = true;
        _objectRigidbody.velocity = throwVelocity;
        SwitchColliders(true);
    }

    private void SwitchColliders(bool collidersValue)
    {
        foreach(Collider coll in _objectColliders)
        {
            coll.enabled = collidersValue;
        }
    }

    public Vector3 GetVelocity()
    {
        return _objectRigidbody.velocity;
    }
}
