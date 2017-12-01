using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPickUp : MonoBehaviour {

	public int value;
	public ParticleSystem ps;
	public GameObject psGo;
	bool collected;

    private void Start ()
    {
		gameObject.tag="Coins";
		ps.Stop ();
	}

    private void Update ()
    {
		if (collected && !ps.isEmitting) {
			Destroy (gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
    {
		if (!collected && other.tag == "Player") {
			collected = true;
			SoundManager.instance.PlayingSound("Coin");

			FindObjectOfType<GameManager> ().AddCoins (value);
			GetComponent<MeshRenderer>().enabled = false;
			ps.Play ();
		}
	}
}
