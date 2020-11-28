using UnityEngine;

public class Flying : MonoBehaviour
{
    public static Flying Instance;
    private void Awake()
    {
        Instance = this;
    }

    public float FlySpeed = 5f;
    public float AccelerationDur = .5f;
    public float DecelerationDur = 1f;
    public float GravityPush = 1f;
    public Vector3 Finalvelo = Vector3.zero;
    private bool EnableFlying = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            EnableFly(!EnableFlying);
        }

        if (!EnableFlying) return;

        Vector3 input = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            input += Vector3.up;
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            input += Vector3.down;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            input += Vector3.left;
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            input += Vector3.right;

        input.Normalize();

        flyVelo = Vector3.SmoothDamp(flyVelo, input * FlySpeed, ref sd_velo, input == Vector3.zero ? DecelerationDur : AccelerationDur);
        flyVelo.y += Time.deltaTime * GravityPush;

    }

    private void FixedUpdate()
    {
        if (!EnableFlying) return;
        PlayerController.Instance.R.velocity = flyVelo;
    }

    public void EnableFly(bool enable)
    {
        PlayerController.Instance.SwitchBasicPhysicsLogics(!enable);
        EnableFlying = enable;
        sd_velo = Vector3.zero;
        flyVelo = Vector3.zero;
    }

    private Vector3 sd_velo = Vector3.zero;
    private Vector3 flyVelo = Vector3.zero;
}
