using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupSceneManager : MonoBehaviour
{
    public void Start()
    {
        LevelsManager.Instance.LoadScene("MainMenu");
    }
}
