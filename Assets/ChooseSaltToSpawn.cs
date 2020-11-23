using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSaltToSpawn : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Salts;

    [SerializeField]
    private Dropdown SaltsDropdown;

    public void InstantiateSalt()
    {
        Instantiate(Salts[SaltsDropdown.value]);
    }
}
