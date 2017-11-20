using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private int Coins =0;
	private int Diamonds =0;
	public Text LabelCoins;
	public GameObject PausePanel;
	public GameObject DiamondCollectedLabel;
	private PlayerController pc;
	private bool IsPaused =false;
	// Use this for initialization
	void Start () {
		
		pc=	GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController> ();
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


		DiamondCollectedLabel.SetActive (pc.isDiamondCollected);
		
	}

	public void AddCoins(int value){
		Coins += value;

		LabelCoins.text="Gold: " + Coins;
	
	}
	public void AddDiamonds(int value){
		Diamonds += value;
		//start short cutscene z

	}

	public void FinalDiamond(){
	
	//start Cutscene 
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
