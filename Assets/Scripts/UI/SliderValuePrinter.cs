using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderValuePrinter : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _sliderValueText;
#pragma warning restore 649

    [Space]
    [SerializeField] private int _digits = 0;

    private void OnEnable()
    {
        UpdateValueText(_slider.value);

        _slider.onValueChanged.AddListener(UpdateValueText);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveAllListeners();
    }

    private void UpdateValueText(float newValue)
    {
        _sliderValueText.text = Math.Round(newValue, _digits).ToString();
    }
}
