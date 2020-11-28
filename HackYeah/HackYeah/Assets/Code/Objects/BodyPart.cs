using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public bool WasClicked = false;

    [SerializeField]
    private CharacterJoint _joint;

    private bool _canBeClicked = false;

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
}
