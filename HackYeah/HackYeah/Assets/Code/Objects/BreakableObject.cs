using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField]
    private float _forceToBreak = 10;

    private List<Rigidbody> _childrenRigidbodies = new List<Rigidbody>();

    public void Start()
    {
        foreach(Rigidbody rbody in GetComponentsInChildren<Rigidbody>())
        {
            _childrenRigidbodies.Add(rbody);
        }
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null)
            return;

        PickableObject pickable = other.attachedRigidbody.GetComponent<PickableObject>();
        if(pickable!=null)
        {
            if(pickable.GetVelocity().magnitude >= _forceToBreak)
            {
                Break(pickable.transform.position, pickable.GetVelocity());
            }
        }

        PlayerController player = other.attachedRigidbody.GetComponent<PlayerController>();
        if(player!=null)
        {
            if(player.GetVelocity().magnitude >= _forceToBreak)
            {
                Break(player.transform.position, player.GetVelocity());
            }
        }
    }

    private void Break(Vector3 position, Vector3 velocity)
    {
        foreach(Rigidbody rbody in _childrenRigidbodies)
        {
            rbody.isKinematic = false;
            rbody.useGravity = true;
            rbody.velocity = velocity * 0.2f;
        }
    }

}
