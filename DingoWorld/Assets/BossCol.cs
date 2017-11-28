using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCol : MonoBehaviour {

	private bool hit;

	public bool isFinal;
	public float destroyTimer=3f;

	public Animator handAnim;

	public GameObject player;
	// Use this for initialization
	void Start () {

	
	}

	// Update is called once per frame
	void Update () {

		if (hit) {

			destroyTimer -= Time.deltaTime;
			if (destroyTimer < 0) {
				destroyTimer = 3f;
				hit = false;

			}



		}

	}

	private void OnTriggerEnter(Collider other){


		if (!hit && other.tag == "Player") {
			hit = true;

			FindObjectOfType<PigBossController> ().Damage();

			//handAnim switch anim




		}

	}
}
