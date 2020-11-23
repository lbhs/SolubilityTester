using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalSpawnerButtonScript : MonoBehaviour
{
    private int instantiationRowIndex;

    private bool canSpawn;

    [SerializeField]
    private GameObject metalToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        instantiationRowIndex = 2;
        canSpawn = true;
    }

    // TODO: Consider electrostatic basis for holding metal particles together
    public void SpawnMetal()
    {
        if (canSpawn)
        {
            for (int i = -1; i < 2; i++)
            {
                Vector3 worldSpaceInstantiationPosition = Camera.main.ScreenToWorldPoint(GetComponent<RectTransform>().transform.position);
                // Setting the z-position to 0 allows the sprite to render on top of the canvas
                worldSpaceInstantiationPosition.z = 0;
                worldSpaceInstantiationPosition.x += 1.35f * i;
                GameObject metal = Instantiate(metalToSpawn);
                metal.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                metal.transform.position = worldSpaceInstantiationPosition;
                metal.GetComponent<Redox>().instantiationColumnPosition = i;
                metal.GetComponent<Redox>().instantiationRowPosition = instantiationRowIndex;
                AddMetalToForcesKeeper(metal);
                StartCoroutine(LimitSpawns(2f));
            }
            instantiationRowIndex--;
            MetalIonSceneController.Instance.MetalChosen();

            //if (instantiationIndex == 1)
            //{
            //    instantiationIndex = -1;
            //    StartCoroutine(LimitSpawns(2f));
            //}
            //else
            //{
            //    instantiationIndex++;
            //}
        }
    }

    public void SpawnIon()
    {
        if (canSpawn)
        {
            Vector3 worldSpaceInstantiationPosition = Camera.main.ScreenToWorldPoint(GetComponent<RectTransform>().transform.position);
            // Setting the z-position to 0 allows the sprite to render on top of the canvas
            worldSpaceInstantiationPosition.z = 0;
            GameObject ion = Instantiate(metalToSpawn);
            ion.transform.position = worldSpaceInstantiationPosition;
            ion.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
            AddCationToForcesKeeper(ion);
            StartCoroutine(LimitSpawns(1f));
        }
    }

    private void AddCationToForcesKeeper(GameObject ion)
    {
        GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().Cations.Add(ion);
        if (!GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().ActiveIons.Contains(ion))
        {   //don't want to duplicate ions in the ActiveIons list!
            GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().ActiveIons.Add(ion);
        }
    }

    private void AddMetalToForcesKeeper(GameObject metal)
    {
        if (!GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().ActiveIons.Contains(metal))
        {   //don't want to duplicate ions in the ActiveIons list!
            GameObject.Find("ForcesKeeper").GetComponent<IonicForceCalculatorScript>().ActiveIons.Add(metal);
        }
    }

    private IEnumerator LimitSpawns(float duration)
    {
        canSpawn = false;
        yield return new WaitForSeconds(duration);
        canSpawn = true;
    }
} 
