using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    private float Input_Accelerate = 0f;

    public void UpdateInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Input_Accelerate = -1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Input_Accelerate = 1f;
        }
        else
            Input_Accelerate = 0f;

        //if (IsGrounded)
        //    if (Input.GetKeyDown(KeyCode.Space))
        //        triggerJumping = 15f;

        //Objects
        if (SearchForPickablesNearPlayer() != null)
            UIPickableHint.Instance.hint.enabled = true;
        else
            UIPickableHint.Instance.hint.enabled = false;

        if (Input.GetKeyDown(KeyCode.E))
            PlayerWantsToPickAnyObject();

        ProcessThrowingInput();

        if (_currentlyPickedObject != null)
            UIThrowHint.Instance.hint.enabled = true;
        else
            UIThrowHint.Instance.hint.enabled = false;

        if (Input.GetKeyDown(KeyCode.L))
        {
            SwitchRagdoll(true, true);
            //AddForceToRagdollBodies(Vector3.right * 10f);
        }

    }

    public void SetJumpInput(float jumpPower)
    {
        triggerJumping = jumpPower;
    }


}
