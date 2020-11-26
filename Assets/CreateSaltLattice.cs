using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSaltLattice : MonoBehaviour
{
    private GameObject spriteToSpawn;
    private IonicForceCalculatorScript forceModule;

    [SerializeField]
    private PhysicMaterial zeroBounce;

    // The layer of the lattice--starts with 0
    private int j;

    private bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        spriteToSpawn = GetComponent<SaltSpawnerButtonScript>().SpriteToSpawn;
        forceModule = FindObjectOfType<IonicForceCalculatorScript>();
        j = 0;
        canSpawn = true;
    }

    // When this method is called, the entire lattice is instantiated
    // I will likely eliminate this, but I will keep this method until I confirm that alternatives work
    public void SpawnSaltLattice()
    {
        // Determine whether two or three ions/salt

        //PassThroughWallScript.zeroBounce = zeroBounce;

        // Case 1: Two ions/salt - 3x4 lattice
        if (spriteToSpawn.GetComponent<SaltFallsIntoSceneCases>().ionFormation == 1)
        {
            Vector3 worldSpaceInstantiationPosition = Camera.main.ScreenToWorldPoint(GetComponent<RectTransform>().transform.position);
            // Setting the z-position to 0 allows the sprite to render on top of the canvas
            worldSpaceInstantiationPosition.z = 0;

            GameObject cation = spriteToSpawn.GetComponent<SaltFallsIntoSceneCases>().DissociationCation;
            GameObject anion = spriteToSpawn.GetComponent<SaltFallsIntoSceneCases>().DissociationAnion;

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector3 instantiationPosition = new Vector3(worldSpaceInstantiationPosition.x + 1.35f * i, worldSpaceInstantiationPosition.y + 1.35f * j);
                    if (j % 2 == 1)
                    {
                        if (i % 2 == 1)
                        {
                            GameObject instantiatedAnion = Instantiate(anion);
                            instantiatedAnion.transform.position = instantiationPosition;
                            instantiatedAnion.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                            instantiatedAnion.AddComponent<PassThroughWallScript>();
                            AddElectroStaticForceToParticle(instantiatedAnion);
                        }
                        else
                        {
                            GameObject instantiatedCation = Instantiate(cation);
                            instantiatedCation.transform.position = instantiationPosition;
                            instantiatedCation.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                            instantiatedCation.AddComponent<PassThroughWallScript>();
                            AddElectroStaticForceToParticle(instantiatedCation);
                        }
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {
                            GameObject instantiatedAnion = Instantiate(anion);
                            instantiatedAnion.transform.position = instantiationPosition;
                            instantiatedAnion.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                            instantiatedAnion.AddComponent<PassThroughWallScript>();
                            AddElectroStaticForceToParticle(instantiatedAnion);
                        }
                        else
                        {
                            GameObject instantiatedCation = Instantiate(cation);
                            instantiatedCation.transform.position = instantiationPosition;
                            instantiatedCation.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                            instantiatedCation.AddComponent<PassThroughWallScript>();
                            AddElectroStaticForceToParticle(instantiatedCation);
                        }
                    }
                }
            }
        }
    }

    // This method needs to be called 
    public void SpawnSaltLatticeLayerByLayer()
    {
        // Case 1: Two ions/salt - 3x4 lattice
        if (spriteToSpawn.GetComponent<SaltFallsIntoSceneCases>().ionFormation == 1)
        {
            GameObject cation = spriteToSpawn.GetComponent<SaltFallsIntoSceneCases>().DissociationCation;
            GameObject anion = spriteToSpawn.GetComponent<SaltFallsIntoSceneCases>().DissociationAnion;

            if (j < 3)
            {
                if (canSpawn)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Vector3 worldSpaceInstantiationPosition = Camera.main.ScreenToWorldPoint(GetComponent<RectTransform>().transform.position);
                        // Setting the z-position to 0 allows the sprite to render on top of the canvas
                        worldSpaceInstantiationPosition.z = 0;

                        worldSpaceInstantiationPosition.x += 1.35f * i;

                        // Modulus operators used to create the alternating pattern
                        // Cu SO4 Cu SO4
                        // SO4 Cu SO4 Cu
                        // Cu SO4 Cu SO4
                        // SO4 Cu SO4 Cu
                        if (j % 2 == 1)
                        {
                            if (i % 2 == 1)
                            {
                                GameObject instantiatedAnion = Instantiate(anion);
                                instantiatedAnion.transform.position = worldSpaceInstantiationPosition;
                                instantiatedAnion.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                                instantiatedAnion.AddComponent<PassThroughWallScript>();
                                AddElectroStaticForceToParticle(instantiatedAnion);
                            }
                            else
                            {
                                GameObject instantiatedCation = Instantiate(cation);
                                instantiatedCation.transform.position = worldSpaceInstantiationPosition;
                                instantiatedCation.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                                instantiatedCation.AddComponent<PassThroughWallScript>();
                                AddElectroStaticForceToParticle(instantiatedCation);
                            }
                        }
                        else
                        {
                            if (i % 2 == 0)
                            {
                                GameObject instantiatedAnion = Instantiate(anion);
                                instantiatedAnion.transform.position = worldSpaceInstantiationPosition;
                                instantiatedAnion.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                                instantiatedAnion.AddComponent<PassThroughWallScript>();
                                AddElectroStaticForceToParticle(instantiatedAnion);
                            }
                            else
                            {
                                GameObject instantiatedCation = Instantiate(cation);
                                instantiatedCation.transform.position = worldSpaceInstantiationPosition;
                                instantiatedCation.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                                instantiatedCation.AddComponent<PassThroughWallScript>();
                                AddElectroStaticForceToParticle(instantiatedCation);
                            }
                        }
                    }

                    j++;


                    // 2 second delay before instantiating next layer
                    StartCoroutine(LimitSpawns(2f));
                }
            }
            else
            {
                // After 5s, instantiate three water molecules
                StartCoroutine(FindObjectOfType<WaterInstantiationController>().InstantiateWaterWithinBounds(5f, 3));
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void AddElectroStaticForceToParticle(GameObject particle)
    {
        if (!forceModule.ActiveIons.Contains(particle))
        {
            forceModule.ActiveIons.Add(particle);
        }
    }

    private IEnumerator LimitSpawns(float duration)
    {
        canSpawn = false;
        yield return new WaitForSeconds(duration);
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
