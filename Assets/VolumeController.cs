using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private AudioSource gameAudioSource;
    private Slider gameAudioVolume;

    [SerializeField]
    private Text volumeText;

    // Start is called before the first frame update
    void Start()
    {
        gameAudioSource = GameObject.Find("MusicController").GetComponent<AudioSource>();
        gameAudioVolume = GetComponent<Slider>();
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        gameAudioSource.volume = gameAudioVolume.value;
        volumeText.text = string.Format("Volume: {0:0}%", 100 * gameAudioVolume.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
