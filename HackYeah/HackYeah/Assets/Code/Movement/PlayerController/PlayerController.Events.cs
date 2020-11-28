using UnityEngine;

public partial class PlayerController
{
    public void OnJump()
    {
        isJumping = true;
    }

    void OnHitGround(Vector3 velo)
    {
        IsGrounded = true;
        isJumping = false;
    }

}
