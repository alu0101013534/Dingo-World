using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickPlatform : MonoBehaviour {


	public GameObject player;
	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {

			player.transform.parent = this.transform;
		}


	}
	void OnTriggerExit(Collider other){

		if (other.tag == "Player") {


			player.transform.parent = null;

		}


	}
}
