using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plat15x15B_controller : MonoBehaviour {

    public Transform rotator;
    public Transform rotator_obstacle;
    public float speed;
	
	void Update () {
        rotator.eulerAngles = new Vector3(rotator.eulerAngles.x, rotator.eulerAngles.y + speed * Time.deltaTime, rotator.eulerAngles.z);
        rotator_obstacle.eulerAngles = new Vector3(rotator_obstacle.eulerAngles.x, rotator_obstacle.eulerAngles.y - speed * Time.deltaTime, rotator_obstacle.eulerAngles.z);
    }
}
