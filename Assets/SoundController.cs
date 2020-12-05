using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioClip success;

    [SerializeField]
    private AudioClip fail;

    [SerializeField]
    private AudioClip splash;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySuccess()
    {
        audioSource.clip = success;
        audioSource.Play();
    }

    public void PlayFailure()
    {
        audioSource.clip = fail;
        audioSource.Play();
    }

    public void PlaySplash()
    {
        audioSource.clip = splash;
        audioSource.Play();
    }
}
