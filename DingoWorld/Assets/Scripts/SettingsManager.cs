
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
    public Slider effectsSlider;



    void onEnable()
    {
        invertXToggle.onValueChanged.AddListener(delegate { OnInvertXToggle(); });
        invertYToggle.onValueChanged.AddListener(delegate { OnInvertYToggle(); });
        sensivitySlider.onValueChanged.AddListener(delegate { OnSensivityChange(); });
        musicSlider.onValueChanged.AddListener(delegate { OnMusicChange(); });
        effectsSlider.onValueChanged.AddListener(delegate { OnEffectsChange(); });

        
    }

    private void OnEffectsChange()
    {
        PlayerPrefs.SetFloat("Effects", effectsSlider.value);
    }

    private void OnMusicChange()
    {
        PlayerPrefs.SetFloat("Musica", musicSlider.value);
    }

    private void OnSensivityChange()
    {
        PlayerPrefs.SetFloat("Sensibilidad", sensivitySlider.value);
    }

    private void OnInvertYToggle()
    {
        if(invertYToggle.isOn == true)
        {
            PlayerPrefs.SetInt("InvertY", -1);
        }
        else
        {
            PlayerPrefs.SetInt("InvertY", 1);
        }

    }

    private void OnInvertXToggle()
    {
       if(invertXToggle.isOn == true)
       {
           PlayerPrefs.SetInt("InvertX", -1);
       }
       else
       {
            PlayerPrefs.SetInt("InvertX", 1);
       }
        
    }


    // Use this for initialization
    void Start () {
        PlayerPrefs.SetFloat("Musica", 1.0f);
        PlayerPrefs.SetFloat("Efectos", 1.0f);
        PlayerPrefs.SetInt("InvertX", 1);
        PlayerPrefs.SetInt("InvertY", 1);
        PlayerPrefs.SetFloat("Sensibilidad", 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
