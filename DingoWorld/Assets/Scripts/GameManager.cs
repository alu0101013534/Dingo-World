using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int Coins =0;
	public Text LabelCoins;
	public GameObject PausePanel;

	private bool IsPaused =false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
		
			IsPaused = !IsPaused;
			PausePanel.SetActive (IsPaused);

			if (IsPaused) {
			
				Time.timeScale = 0f;
			} 
			else {

				Time.timeScale = 1f;
			
			}
		
		}


		
	}

	public void AddCoins(int value){
		Coins += value;

		LabelCoins.text="Gold: " + Coins;
	
	}


	public void Resume(){

		IsPaused = false;
		PausePanel.SetActive (IsPaused);

		Time.timeScale = 1f;

	}

	public void Options(){


	}

	public void Quit(){

		Application.LoadLevel (0);
	}
}
