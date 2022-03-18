using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterInstantiationController : MonoBehaviour
{
    [SerializeField]
    private GameObject waterMolecule;

    [SerializeField]
    private int numWater;

    private IonicForceCalculatorScript ionicForceCalculatorScript;

    // Start is called before the first frame update
    void Start()
    {
        ionicForceCalculatorScript = FindObjectOfType<IonicForceCalculatorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator InstantiateWaterWithinBounds(float delay)
    {
        yield return new WaitForSeconds(delay);

        DynamicBoundaries dynamicBoundaries = FindObjectOfType<DynamicBoundaries>();

        GameObject pauseButton = FindObjectOfType<PauseController>().gameObject;
        pauseButton.GetComponent<Button>().interactable = true;

        for (int i = 0; i < numWater; i++)
        {
            // Instantiate X between the center of the game display and offset from the right wall by 2 units
            // That way, the molecules are not instantiated where the new lattice is
            // Offsetting their positions from the right wall ensures that the molecules are not instantiated directly on the wall, where they get stuck
            float randomXPosition = UnityEngine.Random.Range(4f, dynamicBoundaries.rightBoundary.x - 2f);
            // The randomly-chosen Y position assumes that the waterline is not lowered
            float randomYPosition = UnityEngine.Random.Range(-7f, -2f);
            GameObject water = Instantiate(waterMolecule);
            // Account for solvation
            if (water.transform.childCount > 0)
            {
                // Oxygen
                ionicForceCalculatorScript.ActiveIons.Add(water);
                // H
                ionicForceCalculatorScript.ActiveIons.Add(water.transform.GetChild(0).gameObject);
                // H
                ionicForceCalculatorScript.ActiveIons.Add(water.transform.GetChild(1).gameObject);
            }
            water.transform.position = new Vector3(randomXPosition, randomYPosition, 0);
            water.GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        }
    }
}
