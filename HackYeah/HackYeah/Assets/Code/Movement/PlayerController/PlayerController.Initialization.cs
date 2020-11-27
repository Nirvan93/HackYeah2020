using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    public void Initialize()
    {
        rigbody = GetComponentInChildren<Rigidbody>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        capsuleCollider.material = MFriction;
        rigbody.maxAngularVelocity = Mathf.Infinity;

        currentRotation = transform.rotation;
        targetedRotation = transform.rotation;

        proceduralPosition = transform.position;
    }

}
