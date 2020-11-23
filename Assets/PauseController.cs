using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private bool paused;

    [SerializeField]
    private Text buttonText;

    private float lastTimescale;
   
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    public void PausedButtonHandler()
    {
        paused = !paused;
        if (!paused)
        {
            AudioListener.pause = false;
            buttonText.text = "Pause";
            Time.timeScale = lastTimescale;
        }
        else
        { 
            AudioListener.pause = true;
            buttonText.text = "Play";
            lastTimescale = Time.timeScale;
            Time.timeScale = 0f;
        }
    }
}
