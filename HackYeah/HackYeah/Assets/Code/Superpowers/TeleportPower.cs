using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPower : SuperPower
{
    [SerializeField]
    private float _teleportTime = 1f;
        
    public void Update()
    {
        if (!_isActive)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            StartTeleportingProcedure();
        }
    }

    private void StartTeleportingProcedure()
    {
        StartCoroutine(TeleportRoutine());
    }

    private IEnumerator TeleportRoutine()
    {
        var worldMousePosition = Input.mousePosition;
        worldMousePosition.z = (transform.position.z - TCameraController.Instance.transform.position.z);
        worldMousePosition = TCameraController.Instance.CameraComponent.ScreenToWorldPoint(worldMousePosition);
        worldMousePosition.z = transform.position.z;

        _playerController.transform.rotation = Quaternion.Euler(_playerController.transform.rotation.eulerAngles.x, 180, _playerController.transform.rotation.eulerAngles.z);
        Time.timeScale = 0.0f;
        BodyPartCollection.Instance.StartClicking();

        yield return new WaitForSecondsRealtime(_teleportTime);

        if (!BodyPartCollection.Instance.StopClicking())
        {
            _playerController.SwitchRagdoll(true);
            yield return new WaitForSecondsRealtime(0.2f);
            BodyPartCollection.Instance.AddRigidbodyToNotClickedBodyParts();
            yield return new WaitForSecondsRealtime(0.2f);
        }

        _playerController.transform.position = worldMousePosition;
        _playerController.transform.rotation = Quaternion.Euler(_playerController.transform.rotation.eulerAngles.x, 90, _playerController.transform.rotation.eulerAngles.z);
        Time.timeScale = 1f;
        
    }
}
