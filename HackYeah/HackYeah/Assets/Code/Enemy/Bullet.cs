using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _bulletRigidbody;

    [SerializeField]
    private float _speed = 20;

    public void BulletShot()
    {
        if(_bulletRigidbody!=null)
        {
            _bulletRigidbody.velocity = _speed * transform.forward;
        }
    }
}
