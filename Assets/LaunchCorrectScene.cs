using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaunchCorrectScene : MonoBehaviour
{
    private Dropdown sceneDropdown;

    // Start is called before the first frame update
    void Start()
    {
        sceneDropdown = GetComponent<Dropdown>();
    }

    public void LaunchScene()
    {
        SceneManager.LoadScene(sceneDropdown.value + 1);
    }
}
