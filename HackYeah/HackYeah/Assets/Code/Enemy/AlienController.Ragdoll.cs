using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AlienController
{
    List<Rigidbody> ragBodies;
    List<Collider> ragColliders;
    List<BodyPart> bodyParts;

    public void InitRagdoll()
    {
        ragBodies = new List<Rigidbody>();
        ragColliders = new List<Collider>();

        foreach (var item in GetComponentsInChildren<Transform>(true))
        {
            if (item != transform)
            {
                Rigidbody r = item.GetComponent<Rigidbody>();
                if (r)
                {
                    ragBodies.Add(r);
                    r.interpolation = RigidbodyInterpolation.Interpolate;
                }

                Collider c = item.GetComponent<Collider>();
                if (c) ragColliders.Add(c);
            }
        }

        Collider cc = GetComponent<Collider>();
        for (int i = 0; i < ragBodies.Count; i++)
        {
            CharacterJoint joint = ragBodies[i].GetComponent<CharacterJoint>();
            if (joint != null) joint.breakForce = BreakJointForce;
            if (cc) Physics.IgnoreCollision(cc, ragColliders[i]);
        }

        GetChest().GetComponent<CharacterJoint>().breakForce = Mathf.Infinity;
        GetUpLegR().GetComponent<CharacterJoint>().breakForce = BreakJointForce * 1.25f;
        GetUpLegL().GetComponent<CharacterJoint>().breakForce = BreakJointForce * 1.25f;
        GetUpArmR().GetComponent<CharacterJoint>().breakForce = BreakJointForce * 1.12f;
        GetUpArmL().GetComponent<CharacterJoint>().breakForce = BreakJointForce * 1.12f;

        Physics.IgnoreCollision(GetChest().GetComponent<Collider>(), GetUpArmL().transform.GetChild(0).GetComponent<Collider>());
        Physics.IgnoreCollision(GetChest().GetComponent<Collider>(), GetUpArmR().transform.GetChild(0).GetComponent<Collider>());

        Physics.IgnoreCollision(GetUpLegR().GetComponent<Collider>(), GetUpLegR().transform.GetChild(0).GetComponent<Collider>());
        Physics.IgnoreCollision(GetUpLegL().GetComponent<Collider>(), GetUpLegL().transform.GetChild(0).GetComponent<Collider>());

        Physics.IgnoreCollision(GetUpArmL().GetComponent<Collider>(), GetUpArmL().transform.GetChild(0).GetComponent<Collider>());
        Physics.IgnoreCollision(GetUpArmR().GetComponent<Collider>(), GetUpArmR().transform.GetChild(0).GetComponent<Collider>());

        SwitchRagdoll(false, false);
    }

    public Transform SkelRoot;
    public float BreakJointForce = 10f;
    private bool ragg = false;
    public void SwitchRagdoll(bool turnOnRagdolling, bool isDead)
    {
        ragg = turnOnRagdolling;

        if (SkelRoot) SkelRoot.gameObject.SetActive(turnOnRagdolling);


        for (int i = 0; i < ragColliders.Count; i++)
        {
            //ragColliders[i].enabled = turnOnRagdolling;
            ragBodies[i].velocity = Vector3.down * 0.4f;
        }

        GetComponent<Animator>().enabled = !turnOnRagdolling;
        if (isDead)
            GameUiController.Instance.ShowFadingUI();
    }


    public Transform GetChest() { return FindInRagdoll("chest"); }
    public Transform GetUpLegL() { return FindInRagdoll("upperleg_l"); }
    public Transform GetUpLegR() { return FindInRagdoll("upperleg_r"); }
    public Transform GetUpArmL() { return FindInRagdoll("arm_l"); }
    public Transform GetUpArmR() { return FindInRagdoll("arm_r"); }

    public void AddForceToRagdollBodies(Vector3 power)
    {
        SwitchRagdoll(true, false);

        for (int i = 0; i < ragBodies.Count; i++)
        {
            ragBodies[i].AddForce(power, ForceMode.Impulse);
        }
    }

    public Transform FindInRagdoll(string name)
    {
        for (int i = 0; i < ragBodies.Count; i++)
        {
            if (ragBodies[i].name.ToLower().Contains(name))
            {
                return ragBodies[i].transform;
            }
        }

        return null;
    }

    private void FixedUpdate()
    {
        if (!ragg)
            for (int i = 0; i < ragBodies.Count; i++)
            {
                ragBodies[i].velocity = Vector3.zero;
            }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 12f)
        {
            AddForceToRagdollBodies(collision.relativeVelocity);
            TCameraController.Instance.ScreenShake(collision.relativeVelocity.magnitude / 5f, 0.15f);
        }
        Debug.Log("Enter " + collision.relativeVelocity);
    }
}
