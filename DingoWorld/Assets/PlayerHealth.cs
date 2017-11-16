using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {


	public int currentHealth;
	public int maxHealth;
	private PlayerController pc;
	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		pc = gameObject.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void DamagePlayer(int damage){
		currentHealth--;
		if (currentHealth == 0)
			pc.death ();
	}
}
