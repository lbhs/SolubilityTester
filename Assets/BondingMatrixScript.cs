using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondingMatrixScript : MonoBehaviour
{
    public static int[,] SolubilityChart;

    // Start is called before the first frame update
    void Start()
    {
        SolubilityChart = new int[7, 5];  //x-value = CationIndex (MobileIonScript), y-value = AnionIndex (anionScript)
        SolubilityChart[0, 0] = 0;
        SolubilityChart[0, 1] = 1;
        SolubilityChart[0, 2] = 0;
        SolubilityChart[0, 3] = 0;
        SolubilityChart[1, 0] = 0;
        SolubilityChart[1, 1] = 0;
        SolubilityChart[1, 2] = 0;
        SolubilityChart[1, 3] = 0;
        SolubilityChart[2, 0] = 0;
        SolubilityChart[2, 1] = 1;
        SolubilityChart[2, 2] = 1;
        SolubilityChart[2, 3] = 0;
        SolubilityChart[3, 0] = 0;
        SolubilityChart[3, 1] = 1;
        SolubilityChart[3, 2] = 0;
        SolubilityChart[3, 3] = 0;
        SolubilityChart[4, 0] = 0;
        SolubilityChart[4, 1] = 1;
        SolubilityChart[4, 2] = 0;
        SolubilityChart[4, 3] = 0;
        SolubilityChart[5, 0] = 1;
        SolubilityChart[5, 1] = 1;
        SolubilityChart[5, 2] = 1;
        SolubilityChart[5, 3] = 0;
        SolubilityChart[6, 0] = 1;
        SolubilityChart[6, 1] = 1;
        SolubilityChart[6, 2] = 1;
        SolubilityChart[6, 3] = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
