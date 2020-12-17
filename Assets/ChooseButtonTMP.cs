using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChooseButtonTMP : MonoBehaviour
{
    // Every button in saltDropdown must be named as "[salt name]Button" (e.g. "CuSO4Button")
    // The text of every button must be "[salt name]"
    private TMP_Dropdown saltDropdown;

    [Header("Do not modify Salt Buttons")]
    [SerializeField]
    private List<GameObject> saltButtons;

    [Header("Do not modify Button Images")]
    [SerializeField]
    private List<Sprite> buttonImages;

    [SerializeField]
    private List<bool> buttonEnabled;

    [SerializeField]
    private List<string> buttonText;

    [Header("Explanation Display")]
    [SerializeField]
    private ExplanationDialogControllerTMP explanationDisplay;

    private void Start()
    {
        saltDropdown = GetComponent<TMP_Dropdown>();
        //saltDropdown.ClearOptions();

        //Dropdown.OptionData initialOption = new Dropdown.OptionData();
        //initialOption.text = "Choose a Salt";

        //saltDropdown.options.Add(initialOption);

        foreach (GameObject button in saltButtons)
        {
            int buttonIndex = saltButtons.IndexOf(button);
            if (buttonEnabled[buttonIndex])
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                // Display the salt sprite image if it has been provided
                if (buttonImages[buttonIndex] != null)
                {
                    option.image = buttonImages[buttonIndex];
                }
                option.text = ConvertNameToTMP(buttonText[buttonIndex]);
                saltDropdown.options.Add(option);
            }
        }

        saltDropdown.RefreshShownValue();
    }

    public void ValueChangedHandler()
    {
        // value will be 0 if the "Choose a Salt" default selection has been chosen
        if (saltDropdown.value != 0)
        {
            string textWithFormatting = saltDropdown.options[saltDropdown.value].text;
            // This is a temporary and inefficient fix
            string textWithoutFormatting = textWithFormatting.Replace("<sub>", "").Replace("</sub>", "");
            string buttonName = textWithoutFormatting + "Button";
            Debug.Log("buttonName: " + buttonName);
            foreach (GameObject button in saltButtons)
            {
                if (button.name == buttonName)
                {
                    //if (button.name != FindElementDisplayedByPeer())
                    //{
                    //    explanationDisplay.SetTextWrapper(saltDropdown.options[saltDropdown.value].text);
                    //    button.SetActive(true);
                    //    gameObject.SetActive(false);
                    //}
                    //else
                    //{
                    //    explanationDisplay.NoButtonReplication();
                    //    saltDropdown.value = 0;
                    //}
                    explanationDisplay.SetTextWrapper(saltDropdown.options[saltDropdown.value].text);
                    button.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private string ConvertNameToTMP(string name)
    {
        string result = "";
        for (int i = 0; i < name.Length; i++)
        {
            if (Char.IsDigit(name[i]))
            {
                result += string.Format("<sub>{0}</sub>", name[i]);
            }
            else
            {
                result += name[i];
            }
        }
        return result;
    }
}
