
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class SettingsManager : MonoBehaviour {

    public Toggle invertXToggle;
    public Toggle invertYToggle;
    public Slider sensivitySlider;
    public Slider musicSlider;

    void onEnable()
    {
        invertXToggle.onValueChanged.AddListener(delegate { OnInvertXToggle(); });
        invertYToggle.onValueChanged.AddListener(delegate { OnInvertYToggle(); });
        sensivitySlider.onValueChanged.AddListener(delegate { OnSensivityChange(); });
        musicSlider.onValueChanged.AddListener(delegate { OnMusicChange(); });
    }

    public void OnMusicChange()
    {
        PlayerPrefs.SetFloat("Musica", musicSlider.value);
    }

    public void OnSensivityChange()
    {
        PlayerPrefs.SetFloat("Sensibilidad", sensivitySlider.value);
    }

    public void OnInvertYToggle()
    {
        PlayerPrefs.SetInt("InvertY", invertYToggle.isOn  ? -1 : +1);
    }

    public void OnInvertXToggle()
    {
        PlayerPrefs.SetInt("InvertX", invertXToggle.isOn ? - 1 : +1);
    }
}
