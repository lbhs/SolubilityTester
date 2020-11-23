using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    [Header("Description Text Field")]
    [SerializeField]
    private Text descriptionText;

    [Header("Background Image")]
    [SerializeField]
    private Image backgroundPanelDisplay;

    [Header("Dropdown")]
    [SerializeField]
    private Dropdown callingDropdown;

    [Header("Background Image")]
    [SerializeField]
    private List<Sprite> backgrounds;

    [Header("Descriptions")]
    [SerializeField]
    private List<string> descriptions;

    // Start is called before the first frame update
    void Start()
    {
        DisplayCorrectBackgroundAndDescription();
    }

    public void DisplayCorrectBackgroundAndDescription()
    {
        descriptionText.text = descriptions[callingDropdown.value];
        backgroundPanelDisplay.sprite = backgrounds[callingDropdown.value];
    }
}
