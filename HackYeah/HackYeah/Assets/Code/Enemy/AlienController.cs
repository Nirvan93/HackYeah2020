using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AlienController : MonoBehaviour
{
    [SerializeField]
    private Transform _shotPoint;

    [SerializeField]
    private GameObject _bulletPrefab = null;

    [SerializeField]
    private Vector2 _shotIntervalRange = new Vector2(2f, 5f);

    private float _shotTimer = 0;
    private float _nextShotTime = 0;

    public void Start()
    {
        _nextShotTime = _shotIntervalRange.GetRandomValueInRange();
    }

    public void Update()
    {
        _shotTimer += Time.deltaTime;

        if(_shotTimer>=_nextShotTime)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        //Create bullet
        GameObject bullet = Instantiate(_bulletPrefab, _shotPoint.position, _shotPoint.rotation);
        bullet.GetComponent<Bullet>().BulletShot();
        _shotTimer = 0;
        _nextShotTime = _shotIntervalRange.GetRandomValueInRange();

    }


}
