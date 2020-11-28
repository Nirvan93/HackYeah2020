using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMainMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform _levelsPanel = null;

    public void OnClickNewGame()
    {
        _levelsPanel.gameObject.SetActive(true);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
