using UnityEngine;

public partial class PlayerController
{
    private Animator animator;

    private void Animation_Run()
    {
        animator.CrossFade("Run", 0.2f);
    }

    private void Animation_Jump()
    {
        animator.CrossFade("JumpUp", 0.2f);
    }

    private void Animation_Idle()
    {
        animator.CrossFade("Stand", 0.2f);
    }

    private void Animation_Falling()
    {
        animator.CrossFade("Falling", 0.2f);
    }

    void UpdateAnimating()
    {
        if (Mathf.Abs(rigbody.velocity.x) > Mathf.Abs(rigbody.velocity.y))
        {
            if (rigbody.velocity.x > 0.1f)
                Animation_Run();
            else
                Animation_Idle();
        }
        else if(!IsGrounded)
        {
            if (rigbody.velocity.y > 0.1f)
                Animation_Jump();
            else if (rigbody.velocity.y < 0f)
                Animation_Falling();
        }

    }

}