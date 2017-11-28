using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {


	public int currentHealth;
	public int maxHealth;
	private PlayerController pc;


	//invicibility 
	public float invicibilityLength;
	public float invicibilityCounter;


	public Renderer playerRenderer;

	private float flashCounter;
	public float flashLengh = 0.1f;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		pc = gameObject.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {


		if (invicibilityCounter > 0) {
			invicibilityCounter -= Time.deltaTime;

			flashCounter -= Time.deltaTime;
			if (flashCounter <= 0 && pc.spawned) {
			
				playerRenderer.enabled = !playerRenderer.enabled;
				flashCounter = flashLengh;
			}

			if (invicibilityCounter <= 0) {
				playerRenderer.enabled = true;
			}

		}


		
	}


	public void DamagePlayer(int damage){

		if(invicibilityCounter <= 0){
			currentHealth-=damage;
	        
			if (currentHealth <= 0)
				pc.death ();

			invicibilityCounter = invicibilityLength;


			flashCounter = flashLengh;
		}

	}
	public void HealPlayer(int heal){
		currentHealth=heal;

	}
}
