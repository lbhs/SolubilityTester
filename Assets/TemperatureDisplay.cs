using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureDisplay : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        ValueChangedHandler();
    }

    public void ValueChangedHandler()
    {
        text.text = string.Format("Temperature: {0} °C", slider.value * 10);
    }
}
