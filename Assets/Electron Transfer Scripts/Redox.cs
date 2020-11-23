using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Redox : MonoBehaviour
{
    //private forces mainObject;
    private IonicForceCalculatorScript mainObject;
    private float probability;
    private float tempfactor;
    private Slider temperatureSlider;
    public AudioSource Soundsource;
    public AudioClip Playthis;

    bool isReacting = false;
    [Header("Choose One (choosing none will make this a spectator ion)")]
    public bool isReducingAgent;
    public bool isOxidizingAgent;

    //[Rename("Electrode Potential Eº (Volts)")]
    [Header("Electrode Potential Eº (Volts)")]
    // Change to oxidation potential
    public float oxidationPotential;

    [Header("This is the particle that should replace the current one when the reaction occurs")]
    public GameObject ReactionPrefab;

    public int instantiationColumnPosition;
    public int instantiationRowPosition;

    // Start is called before the first frame update
    void Start()
    {
        // mainObject not necessary
        mainObject = GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>();
        temperatureSlider = GameObject.Find("Canvas/TemperatureSlider").GetComponent<Slider>();
        //Soundsource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        //Playthis = GameObject.Find("Sounds").GetComponent<AudioClip>();
        
    }

    // Hope is to use every ion in the game
    // EP equivalent in MobileIonScript: reductionPotential
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == gameObject.tag)
        //{
        //    // Allow the newly-formed metal to rotate
        //    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //}

        if (collision.gameObject.GetComponent<MobileIonScript>())
        {
            // GetComponent<MobileIonScript>()
            MobileIonScript otherP = collision.gameObject.GetComponent<MobileIonScript>(); //otherP stands for other particle
            StartCoroutine("ReactionDelay");
            //temp = temperatureSlider.value;
            tempfactor = 5.1f/temperatureSlider.value; 
            probability = Random.Range(0.0f,tempfactor);
            print(probability);
            // EP is oxidationPotential, otherP.EP is reductionPotential
            if (probability < oxidationPotential + otherP.reductionPotential)
            {
                    
                //gets positions of both objects
                // offset Rpos potentially? Avoid strange behavior
                Vector3 Rpos = gameObject.transform.position;
                Vector3 Opos = otherP.transform.position;

                GameObject NewObject1;
                GameObject NewObject2;

                if (otherP.ReactionPrefab)
                {
                    MetalIonSceneController.Instance.LaunchReactionVideo(gameObject.name, otherP.name);
                    // ReactionPrefab will need to become part of MobileIonScript
                    //spawn the new objects with the old coordinates but flipped
                    NewObject1 = Instantiate(otherP.ReactionPrefab, Opos, Quaternion.Euler(new Vector3(0f, 90f, 0f)));
                    NewObject2 = Instantiate(ReactionPrefab, Rpos, Quaternion.Euler(new Vector3(0f, 90f, 0f)));

                    // make NewObject2 a trigger for some time to solve positioning issues?
                    NewObject1.GetComponent<SphereCollider>().isTrigger = false;
                    NewObject2.GetComponent<SphereCollider>().isTrigger = false;
                    //NewObject1.GetComponent<Rigidbody>().mass = 5f;
                    //NewObject2.GetComponent<Rigidbody>().mass = 5f;

                    // Determine direction of collision
                    Vector3 collisionDir = collision.GetContact(0).point - transform.position;
                    // Left or Right Column
                    //if (instantiationRowPosition == 0)
                    //{
                    //    if (instantiationColumnPosition == 1)
                    //    {
                    //        float vectorAngle = Vector3.Angle(Vector3.right, collisionDir.normalized);
                    //        NewObject2.GetComponent<Rigidbody>().AddForce(Vector3.right * 15f, ForceMode.Impulse);
                    //        Debug.LogFormat("Hit top right atom at an angle of {0} degrees", vectorAngle);
                    //    }

                    //    else if (instantiationColumnPosition == -1)
                    //    {
                    //        NewObject2.GetComponent<Rigidbody>().AddForce(Vector3.left * 15f, ForceMode.Impulse);
                    //        Debug.Log("Hit top left atom");
                    //    }
                    //}

                    Vector3 NewObject1Pos = NewObject1.GetComponent<Rigidbody>().position;
                    NewObject1.GetComponent<Rigidbody>().position = NewObject2.GetComponent<Rigidbody>().position;
                    NewObject2.GetComponent<Rigidbody>().position = NewObject1Pos;

                    //Flag management
                    //if (otherP.GetComponent<LabelAssigner>().hasFlag == true)
                    //{
                    //    NewObject2.GetComponent<LabelAssigner>().hasFlag = true;
                    //}
                    //else if (gameObject.GetComponent<LabelAssigner>().hasFlag == true)
                    //{
                    //    NewObject2.GetComponent<LabelAssigner>().hasFlag = true;
                    //}

                    //Destroy the old objects
                    mainObject.Cations.Remove(otherP.gameObject);
                    mainObject.ActiveIons.Remove(otherP.gameObject);
                    mainObject.ActiveIons.Remove(gameObject);
                    // Move the Game Objects far away, not destroyed?
                    Destroy(otherP.gameObject);

                    //otherP.gameObject.name = "destroyed";
                    //mainObject.gameObjects.Remove(otherP.gameObject);
                    Destroy(gameObject);

                    //Plays a sound
                    //Soundsource.Play();

                    //The need to rename the gameobject is so that it loses the [P] tag
                    //The tag will automatically re-add the particle to the physics list
                    //If an object is destroyed without being removed from the physics list,
                    //all physics will stop until it is resolved
                }

            }
        }
    }
    IEnumerable ReactionDelay() //sometimes it would glitch out and set isReacting to true anyways, this is a quick and dirty fix
    {
        isReacting = true;
        yield return new WaitForSeconds(0.01f);
    }
}
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
