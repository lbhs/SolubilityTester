using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSaltLattice : MonoBehaviour
{
    private GameObject spriteToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        spriteToSpawn = GetComponent<SaltSpawnerButtonScript>().SpriteToSpawn;
    }

    public void SpawnSaltLattice()
    {
        // Determine whether two or three ions/salt

        // Case 1: Two ions/salt - 6x2 lattice
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
                    if (i % 2 == 1)
                    {
                        GameObject instantiatedAnion = Instantiate(anion);
                        instantiatedAnion.transform.position = instantiationPosition;
                        instantiatedAnion.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                    }
                    else
                    {
                        GameObject instantiatedCation = Instantiate(cation);
                        instantiatedCation.transform.position = instantiationPosition;
                        instantiatedCation.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
