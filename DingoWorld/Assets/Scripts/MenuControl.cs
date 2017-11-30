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


	// Use this for initialization
	void Start () {

		OptionMenu.SetActive(false);
		banFx.SetActive(false);
		banMusic.SetActive(false);




		if(PlayerPrefs.GetInt("isMusicEnabled",1)==1)
		{
			//SoundManager.instance.PlayUI();
			isMusicEnabled=true;
		}
		else
		{
			//SoundManager.instance.StopUI();
			isMusicEnabled=false;
		}

		if(PlayerPrefs.GetInt("isSoundFxEnabled",1)==1)
		{
			isSoundFxEnabled=true;
		}
		else
		{
			isSoundFxEnabled=false;
		}


	}
		

	public void Play(){

		PlayerPrefs.SetInt ("Lifes", 3);
		PlayerPrefs.SetInt ("Score", 0);
		Application.LoadLevel(1);

	}

	void Update(){
		banMusic.SetActive (!isMusicEnabled);
		banFx.SetActive (!isSoundFxEnabled);
		if (!isOptionsSelected) {
			OptionMenu.SetActive (false);
		}
	}
	public void Options(){

		isOptionsSelected=!isOptionsSelected;
		if(isOptionsSelected)
		{

			OptionMenu.SetActive(true);
		}
		else
		{
			OptionMenu.SetActive(false);

		}

	}


	public void Quit(){

		Application.Quit();

	}


	public void SoundFX()
	{

		isSoundFxEnabled=!isSoundFxEnabled;
		if(isSoundFxEnabled)
		{
			PlayerPrefs.SetInt("isSoundFxEnabled", 1);
			banFx.SetActive(false);
		}
		else
		{
			PlayerPrefs.SetInt("isSoundFxEnabled", 0);
			banFx.SetActive(true);

		}
	}
	public void Music()
	{

		isMusicEnabled=!isMusicEnabled;
		if(isMusicEnabled)
		{
			PlayerPrefs.SetInt("isMusicEnabled", 1);
			SoundManager.instance.PlayUI();
			banMusic.SetActive(false);
		}
		else
		{
			PlayerPrefs.SetInt("isMusicEnabled", 0);
			SoundManager.instance.StopUI();
			banMusic.SetActive(true);
		}
	}
}
