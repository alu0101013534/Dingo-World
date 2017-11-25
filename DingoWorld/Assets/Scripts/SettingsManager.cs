
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

    GameObject thecamera;
    CameraController camera;

    GameObject themcontroller;
    GameObject theecontroller;


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
        SoundManager effects = theecontroller.GetComponent<SoundManager>();
        effects.effectsVolume = effectsSlider.value;
    }

    private void OnMusicChange()
    {
        SoundManager music = themcontroller.GetComponent<SoundManager>();
        music.musicVolume = musicSlider.value;
    }

    private void OnSensivityChange()
    {
        CameraController camera = thecamera.GetComponent<CameraController>();
        camera.rotateSpeed = sensivitySlider.value;
    }

    private void OnInvertYToggle()
    {
        if (invertYToggle.isOn == false)
        {
            CameraController camera = thecamera.GetComponent<CameraController>();
            camera.invertYaxis = -1;
        }
        else
        {
            CameraController camera = thecamera.GetComponent<CameraController>();
            camera.invertYaxis = 1;
        }

    }

    private void OnInvertXToggle()
    {
        if(invertXToggle.isOn == false)
        {
            CameraController camera = thecamera.GetComponent<CameraController>();
            camera.invertXaxis = -1;
        }
        else
        {
            CameraController camera = thecamera.GetComponent<CameraController>();
            camera.invertXaxis = 1;
        }
        
    }


    // Use this for initialization
    void Start () {
        thecamera = GameObject.Find("Main Camera");
        themcontroller = GameObject.Find("SoundManager");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
