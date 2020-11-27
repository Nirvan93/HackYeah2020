using UnityEngine;

public partial class PlayerController
{
    public void OnJump()
    {
        isJumping = true;
    }

    void OnHitGround(Vector3 velo)
    {
        isGrounded = true;
        isJumping = false;
    }

}
