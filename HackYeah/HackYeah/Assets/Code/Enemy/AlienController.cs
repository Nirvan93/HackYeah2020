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

    EnemyAlienAudio audio;


    public void Start()
    {
        _nextShotTime = _shotIntervalRange.GetRandomValueInRange();
        InitRagdoll();
        audio = GetComponent<EnemyAlienAudio>();
    }
    public void Update()
    {
        float dist = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);


        if (dist > 50) return;
        
        _shotTimer += Time.deltaTime;

        if (_shotTimer >= _nextShotTime)
        {
            Shoot();
        }

        if (_shotPoint)
        {
            if (!ragg)
            {
                if (dist < 25f)
                {
                    Quaternion targetRot = LookRotation((PlayerController.Instance.GetChest().transform.position + Vector3.up * 3.25f) - _shotPoint.position);
                    _shotPoint.parent.transform.rotation = Quaternion.Slerp(_shotPoint.parent.transform.rotation, targetRot, Time.deltaTime * 1f);

                }
            }
            //_shotPoint.transform.parent.transform.rotation = Quaternion.Euler(pistolOff) *
            //Quaternion.LookRotation();
        }

        //if (Input.GetKeyDown(KeyCode.C))
        //    AddForceToRagdollBodies(Vector3.right * 31f);
    }

    public Quaternion LookRotation(Vector3 direction)
    {
        Quaternion fromTo = Quaternion.FromToRotation
        (
            _shotPoint.parent.transform.TransformDirection(_shotPoint.localPosition).normalized,
            (direction).normalized
        );

        return fromTo * _shotPoint.parent.transform.rotation;
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
