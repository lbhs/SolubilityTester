using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    // Revise masses (11/28/2020):
    // Cl-: 2
    // Cu 2+: 3.5
    // Na+: 1
    // SO4 -2: 5
    // Ag+: 6
    // These are relative masses to water

    public PhysicMaterial bouncyIon;

    private SoundController soundController;

    private GameObject currentlyProcessingIon;

    // Start is called before the first frame update
    void Start()
    {
        soundController = FindObjectOfType<SoundController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Colliding with an ion that has not been removed from the lattice (MobileIonScript is disabled)
        if (collision.gameObject.tag == "Cation"
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
                // Disable x-coordinate constraint
                ion.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

                ion.GetComponent<SphereCollider>().material = bouncyIon;

                soundController.PlaySuccess();
            }
            else
            {
                soundController.PlayFailure();
            }
        }
    }

    public void HydrogenCollidedWithIon(GameObject collidedObject)
    {
        if (collidedObject != currentlyProcessingIon)
        {
            currentlyProcessingIon = collidedObject;

            // Colliding with an ion that has not been removed from the lattice (MobileIonScript is disabled)
            if ((collidedObject.gameObject.tag == "Anion" || collidedObject.gameObject.tag == "Cation")
                && collidedObject.gameObject.GetComponent<MobileIonScript>().enabled == false)
            {
                GameObject ion = collidedObject.gameObject;
                if (ion.GetComponent<MobileIonScript>().solubility == 0)
                {
                    // Disable charge
                    ion.GetComponent<MobileIonScript>().ChargeActive = false;
                    // Enable MobileIonScript to give ion (now neutral) autonomous motion
                    ion.GetComponent<MobileIonScript>().enabled = true;
                    // This is temporary--make the ion follow the water molecule 
                    ion.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
                    // Disable x-coordinate constraint
                    ion.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

                    ion.GetComponent<SphereCollider>().material = bouncyIon;

                    soundController.PlaySuccess();
                }
                else
                {
                    soundController.PlayFailure();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
