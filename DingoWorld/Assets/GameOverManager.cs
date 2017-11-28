using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {


	public GameObject gameOverUI;

	public Text scoreText;
	public float gameOverTimer;
	private int highScore;
	private int score;

	public int lvlToLoad=2;


	// Use this for initialization
	void Start () {

		Cursor.lockState = CursorLockMode.None;
		highScore = PlayerPrefs.GetInt ("HighScore",5);

		score = PlayerPrefs.GetInt ("Score",0);

		if (score > highScore) {
			PlayerPrefs.SetInt ("HighScore", score);
			scoreText.text = "New HighScore\n" + score.ToString ()+ " Diamonds";

		} 
		else {
			scoreText.text = "Score\n" + score.ToString ()+ " Diamonds";
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!gameOverUI.active){
			gameOverTimer -= Time.deltaTime;
			if (gameOverTimer < 0) {
				gameOverUI.SetActive (true);

			}
		}
		
	}


	public void NewGame(){
		PlayerPrefs.SetInt ("Score", 0);
		Application.LoadLevel (1);
	}

	public void Menu(){
	

		Application.LoadLevel (0);
	}
}
