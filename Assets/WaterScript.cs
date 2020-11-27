using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Colliding with an ion that has not been removed from the lattice (MobileIonScript is disabled)
        if ((collision.gameObject.tag == "Anion" || collision.gameObject.tag == "Cation") 
            && collision.gameObject.GetComponent<MobileIonScript>().enabled == false)
        {
            GameObject ion = collision.gameObject;
            if (ion.GetComponent<MobileIonScript>().solubility == 0)
            {
                // Disable charge
                ion.GetComponent<MobileIonScript>().ChargeActive = false;
                // Enable MobileIonScript to give ion (now neutral) autonomous motion
                ion.GetComponent<MobileIonScript>().enabled = true;
                // This is temporary--make the ion follow the water molecule 
                ion.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
