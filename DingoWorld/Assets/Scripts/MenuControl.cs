using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour {

	public GameObject OptionMenu;

	bool isSoundFxEnabled;
	bool isMusicEnabled;

	bool isOptionsSelected=false;

	public GameObject banFx;
	public GameObject banMusic;

	private void Start ()
    {
		OptionMenu.SetActive(false);
		banFx.SetActive(false);
		banMusic.SetActive(false);

		isMusicEnabled = PlayerPrefs.GetInt("isMusicEnabled", 1) == 1;
		isSoundFxEnabled = PlayerPrefs.GetInt("isSoundFxEnabled", 1) == 1;
	}
		

	public void Play()
    {
		PlayerPrefs.SetInt ("Lifes", 3);
		PlayerPrefs.SetInt ("Score", 0);
		Application.LoadLevel(1);
	}

	void Update()
    {
		banMusic.SetActive (!isMusicEnabled);
		banFx.SetActive (!isSoundFxEnabled);
		if (!isOptionsSelected) {
			OptionMenu.SetActive(false);
		}
	}

	public void Options()
    {
		isOptionsSelected=!isOptionsSelected;
		OptionMenu.SetActive(isOptionsSelected);
		OptionMenu.SetActive(false);
	}

	public void Quit(){
		Application.Quit();
	}

	public void SoundFX()
	{
		isSoundFxEnabled=!isSoundFxEnabled;
		PlayerPrefs.SetInt("isSoundFxEnabled", isSoundFxEnabled ? 1 : 0);
		banFx.SetActive(!isSoundFxEnabled);
	}

	public void Music()
	{
		isMusicEnabled = !isMusicEnabled;
        PlayerPrefs.SetInt("isMusicEnabled", isMusicEnabled ? 1 : 0);

        if (isMusicEnabled)
            SoundManager.instance.PlayUI();
        else
            SoundManager.instance.StopUI();

        banMusic.SetActive(!isMusicEnabled);
	}
}
