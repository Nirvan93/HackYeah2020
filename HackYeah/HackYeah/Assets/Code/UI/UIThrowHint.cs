using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIThrowHint : MonoBehaviour
{
    public static UIThrowHint Instance;
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
