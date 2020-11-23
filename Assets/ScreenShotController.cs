using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreenShotController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeScreenShot()
    {
        string fileName = DateTime.Now.ToString("MM-dd-yyyy H:mm");
        ScreenCapture.CaptureScreenshot(fileName);
    }
}
