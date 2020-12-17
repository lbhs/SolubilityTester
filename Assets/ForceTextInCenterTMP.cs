using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForceTextInCenterTMP : MonoBehaviour
{
    [SerializeField]
    private Image imageOfSalt;

    // Start is called before the first frame update
    void Start()
    {
        LeftOrRightJustifyText();
    }

    private void LeftOrRightJustifyText()
    {
        if (imageOfSalt.sprite == null)
        {
            // TextAnchor.MiddleCenter
            GetComponent<TMP_Text>().alignment = TextAlignmentOptions.MidlineJustified;
        }
        else
        {
            GetComponent<TMP_Text>().alignment = TextAlignmentOptions.MidlineLeft;
        }
    }
}
