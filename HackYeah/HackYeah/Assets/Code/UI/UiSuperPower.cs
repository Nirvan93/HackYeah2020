using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSuperPower : MonoBehaviour
{
    public ESuperPowerType SuperPowerType = ESuperPowerType.SuperStrength;

    [SerializeField]
    private float _notActivatedSize = 100;
    [SerializeField]
    private float _activatedSize = 150;

    [SerializeField]
    private RectTransform _powerRect;


    public void SetActivatedValue(bool activated)
    {
        _powerRect.sizeDelta = activated ? new Vector2(_activatedSize, _activatedSize) : new Vector2(_notActivatedSize, _notActivatedSize);
    }
}
