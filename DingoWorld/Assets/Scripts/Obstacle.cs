using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	public int damage;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			FindObjectOfType<PlayerHealth>().DamagePlayer(damage);
		}
	}
}
