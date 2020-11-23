using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalScript : MonoBehaviour
{
    private bool passedThroughTopWall;

    // Start is called before the first frame update
    void Start()
    {
        passedThroughTopWall = false;
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
            GetComponent<SphereCollider>().isTrigger = false; 
            passedThroughTopWall = false;
        }
    }
}
