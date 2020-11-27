using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInstantiationController : MonoBehaviour
{
    [SerializeField]
    private GameObject waterMolecule;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator InstantiateWaterWithinBounds(float delay, int numWater)
    {
        yield return new WaitForSeconds(delay);

        DynamicBoundaries dynamicBoundaries = FindObjectOfType<DynamicBoundaries>();

        for (int i = 0; i < numWater; i++)
        {
            // Instantiate X between the center of the game display and offset from the right wall by 2 units
            // That way, the molecules are not instantiated where the new lattice is
            // Offsetting their positions from the right wall ensures that the molecules are not instantiated directly on the wall, where they get stuck
            float randomXPosition = UnityEngine.Random.Range(0, dynamicBoundaries.rightBoundary.x - 2f);
            // The randomly-chosen Y position assumes that the waterline is not lowered
            float randomYPosition = UnityEngine.Random.Range(-7f, -2f);
            GameObject water = Instantiate(waterMolecule);
            water.transform.position = new Vector3(randomXPosition, randomYPosition, 0);
        }
    }
}
