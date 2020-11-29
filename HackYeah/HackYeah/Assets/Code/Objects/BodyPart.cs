using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public HumanBodyBones LimbId;
    public bool WasClicked = false;
    public GameObject Particles;
    public GameObject Explosion;

    [SerializeField]
    private CharacterJoint _joint;

    private bool _canBeClicked = false;

    private void Start()
    {
        if (_joint == null) _joint = GetComponent<CharacterJoint>();
    }

    public void StartTakingClickInput()
    {
        WasClicked = false;
        _canBeClicked = true;
    }

    public void StopClicking()
    {
        _canBeClicked = false;
    }


    public void BreakIfNotClicked()
    {
        if (!WasClicked)
        {
            _joint.breakForce = 0;
            _joint.GetComponent<Rigidbody>().AddForce(Vector3.up * 50);
        }
    }

    public void OnMouseDown()
    {
        TryClick();
    }

    public void TryClick()
    {
        if (_canBeClicked)
            WasClicked = true;
    }

    private void OnJointBreak(float breakForce)
    {
        if (Particles != null)
        {
            //Particles.SetActive(true);
            GameObject particles = Instantiate(Particles, transform.position, transform.rotation * Quaternion.Euler(90f, 0f, 0f));
            particles.transform.SetParent(transform, true);
            particles.transform.localScale = Vector3.one;

            PlayerAudio.Instance.PlayAudio(PlayerAudio.Instance.BreakBone);

            if (Explosion != null)
            {
                GameObject explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
            }
        }
    }

    private static float lastPlayed = -1999f;
    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastPlayed > 0.1f)
            if (collision.relativeVelocity.magnitude > 3)
            {
                lastPlayed = Time.time;
                PlayerAudio.Instance.PlayAudio(PlayerAudio.Instance.Steps);
            }
    }
}
