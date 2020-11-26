using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughWallScript : MonoBehaviour
{
    private bool passedThroughTopWall;
    public bool chargeActive;
    //public static PhysicMaterial zeroBounce;

    // Start is called before the first frame update
    void Start()
    {
        passedThroughTopWall = false;
        chargeActive = false;
        GetComponent<SphereCollider>().isTrigger = true;
        //Vector3 rot = new Vector3(0f, 90f);
        //GetComponent<Rigidbody>().AddRelativeTorque(rot, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TopWall")
        {
            passedThroughTopWall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (passedThroughTopWall)
        {
            // Possibly enable Ionic Bonding Script for cations at this point?
            GetComponent<SphereCollider>().isTrigger = false;
            passedThroughTopWall = false;
            chargeActive = true;
            //GetComponent<SphereCollider>().material = zeroBounce;
        }
    }
}
