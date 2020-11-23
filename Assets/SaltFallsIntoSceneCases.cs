using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltFallsIntoSceneCases : MonoBehaviour
{
    private Vector3 PositionVector;  //position of the falling sprite
    private float XPosition1;  //XPosition variables are used to instantiate up to 3 ions at different x-positions
    private float XPosition2;
    private float XPosition3;
    public int ionFormation;  //1 = single cation,anion;  2 = two cations, one anion;  3 = one cation, two anions
    private AudioSource SplashDown1;
    private AudioSource SplashDown2;

    public float speedOfFall;  //controls downward speed of falling sprite (for the salt compound)
    public GameObject DissociationCation;  //what cation appears upon dissociation (collision with waterline triggers dissociation)
    public GameObject DissociationAnion;
    private GameObject thisSprite;
    public AudioSource DissolvingSound;  
    //public int NumberOfCations;   //Na2CO3 has two cations, one anion--this variable is used to instantiate the proper number of ions
    //public int NumberOfAnions;
    private int i;  //used for counting loops
    private int j;
    private int f;  //used to store Ionic index value
    private int g;  //used to store Ionic Index value

    private float IonVx;
    private float IonVy;
    private Rigidbody NewCationRB;
    private Rigidbody NewAnionRB;
    private Vector3 IonVelocity;
    private int temp;
    private Rigidbody IonToPutInMotion;
    private bool SplashIndex;

    public bool executing;

    private bool hitWaterline;

    // Start is called before the first frame update
    void Start()
    {
        thisSprite = gameObject;
        temp = GameObject.Find("TemperatureController").GetComponent<TemperatureScript>().Temperature;
        SplashDown1 = GameObject.Find("SplashAudioSource").GetComponent<AudioSource>();
        SplashDown2 = GameObject.Find("Splash2AudioSource").GetComponent<AudioSource>();
        executing = true;
        hitWaterline = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (executing)
        {
            PositionVector = thisSprite.transform.position;
            PositionVector.y -= speedOfFall * Time.deltaTime;
            thisSprite.transform.position = PositionVector;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (executing)
        {
            if (collider.gameObject.tag == "Waterline")
            {
                if (SplashIndex == true)  //this triggers the sound when the sprite hits top of waterline
                {
                    SplashDown1.Play();
                    SplashIndex = false;
                    print("played SplashDown2");
                }
                else
                {
                    SplashDown1.Play();
                    SplashIndex = true;
                    print("played Splashdown1");
                }

                XPosition1 = thisSprite.transform.position.x - 1.5f;
                XPosition2 = thisSprite.transform.position.x;
                XPosition3 = thisSprite.transform.position.x + 1.5f;

                if (ionFormation == 1)
                {
                    SpawnCation(XPosition1);
                    SetVelocityPosition1(NewCationRB);

                    SpawnAnion(XPosition2);
                    SetVelocityPosition2(NewAnionRB);
                }
                else if (ionFormation == 2)
                {
                    SpawnCation(XPosition1);
                    SetVelocityPosition1(NewCationRB);

                    SpawnAnion(XPosition2);
                    SetVelocityPosition2(NewAnionRB);

                    SpawnCation(XPosition3);
                    SetVelocityPosition3(NewCationRB);
                }
                else if (ionFormation == 3)
                {
                    SpawnAnion(XPosition1);
                    SetVelocityPosition1(NewAnionRB);

                    SpawnCation(XPosition2);
                    SetVelocityPosition2(NewCationRB);

                    SpawnAnion(XPosition3);
                    SetVelocityPosition3(NewAnionRB);
                }

                Destroy(thisSprite);  //the falling salt compound is a sprite--now changed into 3-D spheres
                                      //DissolvingSound.Play 

            }
        }
    }

    void SpawnCation(float XPos)
    {
        GameObject NewCation = Instantiate(DissociationCation, new Vector3(XPos, PositionVector.y, 0f), Quaternion.Euler(new Vector3(0f, 90f, 0f))) as GameObject;
        GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().Cations.Add(NewCation);  //this is the list of all cations in the scene
        NewCationRB = NewCation.GetComponent<Rigidbody>();
        print(NewCation + "added to Cations list");
        //NewCation.transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
        // Use AnionsInSolution instead of Anions
        // Alternatively use a boolean
        foreach (GameObject Anion in GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().Anions)
        {
            //search for possible bonding partners among the existing Anions in the scene
            f = DissociationCation.GetComponent<IonicBondingScript>().CationIndex;
            g = Anion.GetComponent<AnionScript2>().AnionIndex;  //Cation and AnionIndex are used to scan the Bonding Matrix (BondingMatrixScript) 


            //Need to search the bonding matrix for insoluble bonding partners
            if (BondingMatrixScript.SolubilityChart[f, g] == 1)   //1 means Insoluble,  0 means Soluble compound
            {   //if insoluble combo exists
                print("Cation charge activated");
                NewCation.GetComponent<MobileIonScript>().ChargeActive = true;  //this enables the particle to have electrostatic forces calculated
                if (!GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().ActiveIons.Contains(NewCation))
                {
                    GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().ActiveIons.Add(NewCation); //Active Ions experience electrostatic forces
                    print("Added" + NewCation + "to ActiveIons list");
                    //COULD TRY TO STREAMLINE SCRIPT BY ACTIVATING A SECOND COPY OF THE CATION INSTANTLY, BUT NO SIGN OF TAXING PROCESSOR SO FAR
                }

                Anion.GetComponent<MobileIonScript>().ChargeActive = true;  //this activates the anions already in scene
                if (!GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().ActiveIons.Contains(Anion))
                {   //don't want to duplicate ions in the ActiveIons list!
                    GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().ActiveIons.Add(Anion);
                    print("added to ActiveAnions" + Anion);
                }

            }

        }

    }


    void SpawnAnion(float XPos)
    {
        GameObject NewAnion = Instantiate(DissociationAnion, new Vector3(XPos, PositionVector.y, 0f), Quaternion.Euler(new Vector3(0f, 90f, 0f))) as GameObject;
        GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().Anions.Add(NewAnion);
        print("added" +NewAnion + "to Anions list");
        NewAnionRB = NewAnion.GetComponent<Rigidbody>();
        //NewAnion.transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);

        foreach (GameObject Cation in GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().Cations)
        {
            //search for possible bonding partners among the existing Anions in the scene
            f = Cation.GetComponent<IonicBondingScript>().CationIndex;
            g = DissociationAnion.GetComponent<AnionScript2>().AnionIndex;  //this is used to determine whether a bonding partner exists in the scene

            //Need to search the bonding matrix for potential bonding partners
            if (BondingMatrixScript.SolubilityChart[f, g] == 1)   //1 means Insoluble,  0 means Soluble compound
            {
                print("Anion charge activated");
                NewAnion.GetComponent<MobileIonScript>().ChargeActive = true;  //this enables the particle to have electrostatic forces calculated
                if (!GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().ActiveIons.Contains(NewAnion))
                {
                    GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().ActiveIons.Add(NewAnion); //Active Ions experience electrostatic forces
                    print("added" + NewAnion + "to ActiveIons list");
                }
                        

                Cation.GetComponent<MobileIonScript>().ChargeActive = true;  //this activates the anions already in scene
                if (!GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().ActiveIons.Contains(Cation))
                {   //don't want to duplicate ions in the ActiveIons list--will make calculations too cumbersome
                    GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().ActiveIons.Add(Cation);
                    print("added to ActiveCations" + Cation);
                }
                //RootAnion = Anion.GetComponent<SaltFallsIntoScene>().DissociationAnion;  RootAnion.GetComponent<MobileIonScript>().ChargeActive = true
            }
        }
    }

    void SetVelocityPosition1(Rigidbody IonIdentity)  //the first ion goes toward the left
    {
        IonVx = UnityEngine.Random.Range(-5f, -1f);
        IonVy = UnityEngine.Random.Range(-5f, -1f);
        print("Ion1 Vx = " + IonVx);
        print("Ion1 Vy =" + IonVy);
        IonVelocity = new Vector3(IonVx, IonVy, 0);

        IonToPutInMotion = IonIdentity;
        IonToPutInMotion.velocity = IonVelocity.normalized * temp;
    }

    void SetVelocityPosition2(Rigidbody IonIdentity)  //the second ion goes close to straight down (bit to the right)
    {
        IonVx = UnityEngine.Random.Range(0.5f, 1.5f);
        IonVy = UnityEngine.Random.Range(-5f, -3f);
        print("Ion2 Vx = " + IonVx);
        print("Ion2 Vy = " + IonVy);
        IonVelocity = new Vector3(IonVx, IonVy, 0);
        print(IonVelocity);
         
        IonToPutInMotion = IonIdentity;
        IonToPutInMotion.velocity = IonVelocity.normalized * temp;
    }

    void SetVelocityPosition3(Rigidbody IonIdentity)  //the third ion goes toward the right
    {
        IonVx = UnityEngine.Random.Range(2f, 5f);
        IonVy = UnityEngine.Random.Range(-3f, -1f);
        print("Ion 3 Vx = " + IonVx);
        IonVelocity = new Vector3(IonVx, IonVy, 0);

        IonToPutInMotion = IonIdentity;
        IonToPutInMotion.velocity = IonVelocity.normalized * temp;
    }
}
