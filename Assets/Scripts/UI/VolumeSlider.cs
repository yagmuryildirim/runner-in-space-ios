using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string audioParameter;
    [SerializeField] private float multiplier = 25f;

    public void SetupSlider()
    {
        slider.onValueChanged.AddListener(SliderValueChange);
        slider.minValue = 0.001f;
        slider.value = PlayerPrefs.GetFloat(audioParameter, slider.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(audioParameter, slider.value);
    }

    private void SliderValueChange(float value)
    {
        audioMixer.SetFloat(audioParameter, Mathf.Log10(value) * multiplier);
    }
}
