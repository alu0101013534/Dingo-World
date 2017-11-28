using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public ParticleSystem ps;
	bool hit;

	public GameObject psGo;

	// Use this for initialization
	void Start () {
		gameObject.tag="Coins";
		ps.Stop ();
	}

	// Update is called once per frame
	void Update () {

		if (hit && !ps.isEmitting) {
			Destroy (gameObject);
		}

	}

	private void OnTriggerEnter(Collider other){


		if (!hit && other.tag == "Player" ) {
			hit = true;

			// Explode sound SoundManager.instance.PlayingSound("Coin");

			GetComponent<MeshRenderer>().enabled = false;

			ps.Play ();
			psGo.transform.parent =null;
			//Destroy (gameObject);

		}

	}

}
