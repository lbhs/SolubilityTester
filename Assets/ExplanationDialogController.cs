﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationDialogController : MonoBehaviour
{
    private Text explanationText;

    // Start is called before the first frame update
    void Start()
    {
        explanationText = GetComponent<Text>();
    }

    public void SetTextWrapper(string name)
    {
        StartCoroutine(SetText(name));
    }

    private IEnumerator SetText(string name)
    {
        explanationText.text = string.Format("Click {0}", name);
        yield return new WaitForSeconds(12f);
        explanationText.text = "";
    }

    public void NoButtonReplication()
    {
        explanationText.text = "Choose a different button!";
    }
}
