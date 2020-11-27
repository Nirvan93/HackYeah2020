using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHelper : MonoBehaviour
{
    [SerializeField]
    private KeyCode _keyCodeForScreenshot = KeyCode.Q;

    private int _index = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_keyCodeForScreenshot))
        {
            ScreenCapture.CaptureScreenshot("D://Screenshot" + _index + ".png");
            _index += 1;
        }
    }

    
}
