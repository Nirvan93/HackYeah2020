using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTestHelper : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10;

    private Rigidbody _rigidbody = null;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _rigidbody.velocity = Vector3.right * horizontal * _speed;
    }
}
