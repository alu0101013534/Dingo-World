using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plat15x15A_controller : MonoBehaviour {

    public Transform rotator;
    public float speed;
	
	void Update () {
        rotator.eulerAngles = new Vector3(rotator.eulerAngles.x, rotator.eulerAngles.y + speed * Time.deltaTime, rotator.eulerAngles.z);
	}
}
