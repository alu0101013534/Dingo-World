using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPickUp : MonoBehaviour {

	public int value;
	public ParticleSystem ps;
	public GameObject psGo;
	bool collected;

	// Use this for initialization
	void Start () {
		ps.Stop ();
	}
	
	// Update is called once per frame
	void Update () {

		if (collected && !ps.isEmitting) {
			Destroy (gameObject);
		}
		
	}

	private void OnTriggerEnter(Collider other){
	
	
		if (!collected && other.tag == "Player") {
			collected = true;
			SoundManager.instance.PlayingSound("Coin");

			FindObjectOfType<GameManager> ().AddCoins (value);
			GetComponent<MeshRenderer>().enabled = false;

			ps.Play ();
			//Destroy (gameObject);
		
		}
	
	}

}
