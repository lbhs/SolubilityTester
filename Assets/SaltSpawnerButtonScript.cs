using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaltSpawnerButtonScript : MonoBehaviour
{
    public GameObject SpriteToSpawn;
    public static GameObject LastSpriteInstantiated;
    public int NumberOfClicks;

    // Start is called before the first frame update
    void Start()
    {
        NumberOfClicks = 0;  
    }

    public void SpawnSalt()
    {
        if (LastSpriteInstantiated == null)
        {
            if(NumberOfClicks < 4)   //limit the number of button clicks to 4 (Only 4 instantiation events for a given salt)
            {
                // This technique works because the canvas is set to Screen Space - Overlay
                Vector3 worldSpaceInstantiationPosition = Camera.main.ScreenToWorldPoint(GetComponent<RectTransform>().transform.position);
                // Setting the z-position to 0 allows the sprite to render on top of the canvas
                worldSpaceInstantiationPosition.z = 0;
                GameObject GO = Instantiate(SpriteToSpawn);
                GO.transform.position = worldSpaceInstantiationPosition;
                //GO.transform.localScale = transform.localScale;
                LastSpriteInstantiated = GO;
                NumberOfClicks++;

            }

            
        }
    }




}
