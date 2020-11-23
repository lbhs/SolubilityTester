using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonicBondingScript : MonoBehaviour
{
    public int CationIndex;
    //public int AnionIndex;
    private int a;
    private int b;
    private GameObject BondingPartner;
    public AudioSource AnionCollidedSound;
    public PhysicMaterial NonBounceMaterial;
    private bool alreadyBonded;
    private float localCharge;
    private List<GameObject> previousCollidedObjects;


    // Start is called before the first frame update
    void Start()
    {
        previousCollidedObjects = new List<GameObject>();
        localCharge = GetComponent<MobileIonScript>().charge;
        AnionCollidedSound = GameObject.Find("AnionCollisionAudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown("a"))
        {
            for (i = 0; i < GameObject.FindActiveIons.Count; i++)
            {
                print(ActiveIons[i]);
            }

        } */       
    }

    private void OnCollisionEnter(Collision collider)  //this script is attached only to Cations!
    {
        a = gameObject.GetComponent<IonicBondingScript>().CationIndex;
        BondingPartner = collider.transform.root.gameObject;
        if (BondingPartner.tag == "Anion")
        {
            b = BondingPartner.GetComponent<AnionScript2>().AnionIndex;
            //print("solubility test" + BondingMatrixScript.SolubilityChart[a, b]);
            // Problem: From a purely asthetic point of view, this code functions properly
            // However, suppose cation A collides with anion B when its net charge is -2, bringing its net charge to -1
            // Now, when cation C collides with B, it will successfully form a precipitate, but when A collides with B again, it cannot form a precipitate including A because the anion's net charge is now 0
            if (BondingMatrixScript.SolubilityChart[a, b] == 1 && BondingPartner.GetComponent<AnionScript2>().netCharge != 0)   //1 means Insoluble,  0 means Soluble compound
            {
                if (!previousCollidedObjects.Contains(BondingPartner))
                {
                    BondingPartner.GetComponent<AnionScript2>().netCharge += localCharge;
                    previousCollidedObjects.Add(BondingPartner);
                }
                
                //print("insoluble compound");
                if(alreadyBonded == false)
                {
                    AnionCollidedSound.Play();
                }

                //print("bonding partner = " + BondingPartner);
                
                gameObject.GetComponent<MobileIonScript>().enabled = false;
                BondingPartner.GetComponent<MobileIonScript>().enabled = false;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                BondingPartner.GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Collider>().material = NonBounceMaterial;
                BondingPartner.GetComponent<Collider>().material = NonBounceMaterial;
                alreadyBonded = true;
                gameObject.GetComponent<Rigidbody>().mass = 2;
                BondingPartner.GetComponent<Rigidbody>().mass = 2;


            }
        }

    }




}
