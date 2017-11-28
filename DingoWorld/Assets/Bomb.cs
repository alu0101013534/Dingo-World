using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public ParticleSystem ps;
	bool hit;

	public float speed=10.0f;
	public GameObject psGo;


	public Transform target;

	private float timer= 2f;

	// Use this for initialization
	void Start () {
		ps.Stop ();

		target=GameObject.FindGameObjectWithTag ("Boss").transform;
	}

	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		if(target!=null)
			transform.position = Vector3.MoveTowards (transform.position, target.position, step);



		if (hit) {
			timer -= Time.deltaTime;
			if (timer < 0) {
				Destroy (psGo);
				Destroy (gameObject);

			}
		
		}



	}

	private void OnTriggerEnter(Collider other){


		if (!hit && other.tag == "Boss" ) {
			hit = true;

			// Explode sound SoundManager.instance.PlayingSound("Coin");

			SoundManager.instance.PlayingSound("Exp");
			GetComponent<MeshRenderer>().enabled = false;
			FindObjectOfType<PigBossController> ().Damage();
			ps.Play ();
			psGo.transform.parent =null;
			//Destroy (gameObject);

		}

	}

}
