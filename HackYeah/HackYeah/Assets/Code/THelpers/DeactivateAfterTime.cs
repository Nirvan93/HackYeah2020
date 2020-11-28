using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterTime : MonoBehaviour
{
    [SerializeField]
    private float _deactivationTime = 5f;

    [SerializeField]
    private bool _destroy = false;

    private void OnEnable()
    {
        StartCoroutine(Deactivate());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(_deactivationTime);
        if (_destroy)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}
