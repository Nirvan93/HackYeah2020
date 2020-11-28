using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiLevelButton : MonoBehaviour
{
    [SerializeField]
    private int _levelId = 1;

    [SerializeField]
    private TextMeshProUGUI _levelCaption=null;

    [SerializeField]
    private Button _levelButton = null;

    public void OnClickLevel()
    {
        LevelsManager.Instance.SelectedLevel(_levelId);
    }

    public void Start()
    {
        
    }


    public void OnValidate()
    {
        if (_levelCaption != null)
            _levelCaption.text = "Level " + _levelId;
    }
}
