using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPickableHint : MonoBehaviour
{
    public static UIPickableHint Instance;
    public TextMeshProUGUI hint;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hint.enabled = false;
    }

}
