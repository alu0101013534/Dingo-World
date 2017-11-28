using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private int Coins =0;
	private int Diamonds =0;
	public Text LabelCoins;
	public Text LabelDiamonds;
	public Text LabelLives;
	public GameObject PausePanel;
	public GameObject DiamondCollectedLabel;

	public GameObject SettingsPanel;
	private PlayerController pc;
	private bool IsPaused =false;
	private int totalCoins;
	private bool found;
	private int previousTotalCoins;

	private 
	// Use this for initialization
	void Start () {
		
		pc=	GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController> ();

		//totalCoins=GameObject.FindGameObjectsWithTag ("Coins").Length;

		LabelCoins.text=Coins+"/"+totalCoins;
		Diamonds = PlayerPrefs.GetInt ("Score", 0);
	}
	
	// Update is called once per frame
	void Update () {



		if(!found)
			totalCoins=GameObject.FindGameObjectsWithTag ("Coins").Length;

		if (!found && previousTotalCoins != totalCoins) {
			previousTotalCoins = totalCoins;
		} else {
			found = true;

			LabelCoins.text=Coins+"/"+totalCoins;

		}

		if (Input.GetKeyDown (KeyCode.Escape) && SettingsPanel.activeSelf == false) {
		
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


		LabelLives.text=pc.lives.ToString();



		LabelDiamonds.text=Diamonds.ToString();
		
	}

	public void AddCoins(int value){
		Coins += value;

		LabelCoins.text=Coins+"/"+totalCoins;

		if (Coins == totalCoins) {
			
			pc.addLive ();
		}
	
	}
	public void AddDiamonds(int value){
		Diamonds += value;


		LabelDiamonds.text=Diamonds.ToString();

	}

	public void GameOver(){

		PlayerPrefs.SetInt ("Lifes", 3);
		PlayerPrefs.SetInt ("Score", Diamonds);
		Application.LoadLevel (2);
	
	}


	public void FinalDiamond(){
	

		PlayerPrefs.SetInt ("Lifes", pc.lives);
		PlayerPrefs.SetInt ("Score", Diamonds);
		Application.LoadLevel (1);
	}

	public void Resume(){

		IsPaused = false;
		PausePanel.SetActive (IsPaused);

		Time.timeScale = 1f;

	}

	public void Options(){

        IsPaused = false;
        PausePanel.SetActive(IsPaused);
        Time.timeScale = 0f;
        SettingsPanel.SetActive (true);
        


	}

    public void ExitOptions()
    {
        SettingsPanel.SetActive(false);
        Time.timeScale = 1f;
    }

	public void Quit(){

		Application.LoadLevel (0);
	}
}
