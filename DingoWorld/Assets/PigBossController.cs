using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PigBossController : MonoBehaviour {



	public int health = 3 ;
	public Animator pigAnimator;
	public Animator leftHandAnimator;
	public Animator rightHandAnimator;

	private bool death;
	private float deathTimer= 2.0f;

	public Text hpLabel;

	public GameObject diamond;

	public GameObject lHand;
	public GameObject rHand;


	public GameObject lCol;
	public GameObject rCol;
	// Use this for initialization
	void Start () {

		diamond.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {

		hpLabel.text = health.ToString ();

		if (death) {
			deathTimer -= Time.deltaTime;
			if (deathTimer < 0) {
				//death animation




				diamond.SetActive (true);
				Destroy (lHand);
				Destroy (rHand);
				Destroy (gameObject);
			}

		}
		
	}
		

	public void Damage(){
	
		health--;
		if (!death && health == 0) {
			death = true;

			hpLabel.text = health.ToString ();

			//call animators;
			rCol.SetActive(false);
			lCol.SetActive (false);
			pigAnimator.Play ("pigDeath");
			leftHandAnimator.Play ("leftDeath");
			rightHandAnimator.Play ("rightDeath");

		}
	}
}
