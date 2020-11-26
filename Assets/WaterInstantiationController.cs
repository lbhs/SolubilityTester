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
            // X is defined within the boundaries
            float randomXPosition = UnityEngine.Random.Range(dynamicBoundaries.leftBoundary.x, dynamicBoundaries.rightBoundary.x);
            float randomYPosition = UnityEngine.Random.Range(-7f, -2f);
            GameObject water = Instantiate(waterMolecule);
            water.transform.position = new Vector3(randomXPosition, randomYPosition, 0);
        }
    }
}
