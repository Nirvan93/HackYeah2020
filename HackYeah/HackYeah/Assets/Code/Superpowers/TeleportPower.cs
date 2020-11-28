using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPower : SuperPower
{
    [SerializeField]
    private float _teleportTime = 1f;

    private bool _teleportInProgress = false;

    public void Update()
    {
        if (!_isActive)
            return;

        if (Input.GetMouseButtonDown(1) && !_teleportInProgress)
        {
            StartTeleportingProcedure();
        }

        CheckBodyPartsRaycasts();
    }

    private void StartTeleportingProcedure()
    {
        _teleportInProgress = true;
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

        //_playerController.SwitchRagdoll(true);
        BodyPartCollection.Instance.StartClicking();

        yield return new WaitForSecondsRealtime(_teleportTime);

        if (!BodyPartCollection.Instance.StopClicking())
        {
            yield return new WaitForSecondsRealtime(0.2f);
            BodyPartCollection.Instance.AddRigidbodyToNotClickedBodyParts();
            yield return new WaitForSecondsRealtime(0.2f);
        }
        else
        {
            //_playerController.SwitchRagdoll(false);
        }

        _playerController.transform.position = worldMousePosition;
        _playerController.transform.rotation = Quaternion.Euler(_playerController.transform.rotation.eulerAngles.x, 90, _playerController.transform.rotation.eulerAngles.z);
        Time.timeScale = 1f;
        _teleportInProgress = false;
    }

    private void CheckBodyPartsRaycasts()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = TCameraController.Instance.CameraComponent.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                BodyPart bodyPart = hit.collider.GetComponent<BodyPart>();
                if (bodyPart != null)
                    bodyPart.TryClick();
            }
        }
    }

}
