using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJumpPower : SuperPower
{
    [SerializeField]
    private KeyCode _jumpKeyCode = KeyCode.Space;

    [SerializeField]
    private float _normalJumpPower = 8;

    [SerializeField]
    private float _superJumpPowerMultiplier = 20f;

    private bool _jumpKeyPressed = false;
    private float _jumpKeyPressedTime = 0;

    public void Update()
    {
        if (!_isActive)
        {
            ProcessNormalJumpInput();
        }
        else
        {
            ProcessSuperJumpInput();
        }
    }

    private void ProcessNormalJumpInput()
    {
        if (Input.GetKeyDown(_jumpKeyCode) &&  _playerController != null && _playerController.IsGrounded)
            _playerController.SetJumpInput(_normalJumpPower);
    }

    private void ProcessSuperJumpInput()
    {
        if(_jumpKeyPressed)
        {
            _jumpKeyPressedTime += Time.deltaTime;
            if(Input.GetKeyUp(_jumpKeyCode))
            {
                if (_playerController != null && _playerController.IsGrounded)
                    _playerController.SetJumpInput(_jumpKeyPressedTime * _superJumpPowerMultiplier);

                _jumpKeyPressed = false;
                _jumpKeyPressedTime = 0;
            }
        }

        if (Input.GetKeyDown(_jumpKeyCode) && _playerController != null && _playerController.IsGrounded)
        {
            _jumpKeyPressed = true;
        }

    }
}
