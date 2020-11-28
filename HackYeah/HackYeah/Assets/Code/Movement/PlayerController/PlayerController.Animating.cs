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

}
