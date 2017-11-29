
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
        if(invertYToggle.isOn == true)
        {
            PlayerPrefs.SetInt("InvertY", -1);
        }
        else
        {
            PlayerPrefs.SetInt("InvertY", 1);
        }

    }

    public void OnInvertXToggle()
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
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
