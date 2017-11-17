using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	public Transform origin;
	public Transform target;
	private bool isReturning;
	public float speed=2.0f;
	public GameObject player;
	void start(){

		origin = transform;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	void Update() {
		float step = speed * Time.deltaTime;
		if (!isReturning) {
			transform.position = Vector3.MoveTowards (transform.position, target.position, step);
		} else {
		

			transform.position = Vector3.MoveTowards (transform.position, origin.position, step);
		}
		if (transform.position == target.position) {
			isReturning = true;
		}
		if (transform.position == origin.position) {
			isReturning = false;
		}

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
