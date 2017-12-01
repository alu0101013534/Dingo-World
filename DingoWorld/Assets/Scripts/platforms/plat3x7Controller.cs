using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plat3x7Controller : MonoBehaviour {

    private List<Transform> gears;
    private List<Transform> gearsReverse;
    private GameObject player;
    public float speed = 20;
    private bool playerIn;

	void Start ()
    {
        playerIn = false;
        player = GameObject.FindGameObjectWithTag("Player");
        gears = new List<Transform>();
        gearsReverse = new List<Transform>();
        foreach (Transform item in transform)
        {
            if (item.name.Contains("gear"))
            {
                if (item.name.Contains("reverse"))
                {
                    gears.Add(item);
                }
                else
                {
                    gearsReverse.Add(item);
                }
            }
        }
	}
	
	void Update ()
    {
		foreach (Transform gear in gears)
        {
            gear.Rotate(new Vector3(0f, 0f, Time.deltaTime * +speed));
        }
        foreach (Transform gear in gearsReverse)
        {
            gear.Rotate(new Vector3(0f, 0f, Time.deltaTime * -speed));
        }
        if (playerIn)
        {
            player.transform.Translate(new Vector3(0f, 0f, -.1f * Time.deltaTime * speed), Space.Self);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIn = true;
            player.transform.parent = this.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIn = false;
            player.transform.parent = null;
        }
    }
}
