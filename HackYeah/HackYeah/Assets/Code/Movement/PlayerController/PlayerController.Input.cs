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

        if (isGrounded)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("jumnpoasf");
                triggerJumping = 8f;
            }
    }


}
