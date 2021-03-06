﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour {

	private bool collected;

	public bool isFinal;
	public float destroyTimer=2f;

	private Animator anim;

	public GameObject player;
	// Use this for initialization
	void Start () {

		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (collected) {

			destroyTimer -= Time.deltaTime;
			if (destroyTimer < 0) {
				if(isFinal)
					FindObjectOfType<GameManager> ().FinalDiamond();


				Destroy (gameObject);

			}



		}
		
	}

	private void OnTriggerEnter(Collider other){


		if (!collected && other.tag == "Player") {
			collected = true;

			collected = true;
			SoundManager.instance.PlayingSound("Diamond");
			FindObjectOfType<GameManager> ().AddDiamonds (1);
	
			FindObjectOfType<PlayerController> ().DiamondCollected();

			gameObject.transform.parent = player.transform;
		
			transform.localPosition =  new Vector3 (0, 2f, 0f);




		}

	}
}
