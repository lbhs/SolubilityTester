using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnionScript2 : MonoBehaviour
{
    public int AnionIndex;
    public float netCharge;

    // Start is called before the first frame update
    void Start()
    {
        netCharge = GetComponent<MobileIonScript>().charge;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
