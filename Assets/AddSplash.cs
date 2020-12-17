using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSplash : MonoBehaviour
{
    private bool hasPlayedSound;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayedSound = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Anion" || other.gameObject.tag == "Cation")
        {
            if(!hasPlayedSound)
            {
                FindObjectOfType<SoundController>().PlaySplash();
                hasPlayedSound = true;
            }
        }
    }
}
