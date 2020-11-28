using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintTextController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _hintText = null;

    public void SetHintForSuperPower(ESuperPowerType superPower)
    {
        switch (superPower)
        {
            case ESuperPowerType.SuperSpeed:
                _hintText.text = "Use mouse left click to draw your super speed route!";
                break;
            case ESuperPowerType.SuperJump:
                _hintText.text = "Press space longer to jump higher!";
                break;
            case ESuperPowerType.SuperStrength:
                _hintText.text = "Press right mouse button longer to throw objects stronger";
                break;
            case ESuperPowerType.Flying:
                _hintText.text = "Use WSAD to fly!";
                break;
            default:
                _hintText.text = "";
                break;
        }
    }
}
