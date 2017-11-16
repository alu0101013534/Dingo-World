using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	
	public bool useOffsetValues;
	public float rotateSpeed;
	
	public Transform pivot;


	public float maxAngle;
	public float minAngle;

	public bool invertY;

	// Use this for initialization
	void Start () {
		if(!useOffsetValues)
		{
			offset=target.position-transform.position;
		}
		
		pivot.transform.position=target.transform.position;
		//pivot.transform.parent=target.transform;
		
		pivot.transform.parent=null;

		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		pivot.transform.position=target.transform.position;
		//get x position of the mouse and rotate
		float horizontal = Input.GetAxis("Mouse X")* rotateSpeed;
		pivot.Rotate(0,horizontal,0);
		//get y position of the mouse and rotate
		float vertical = Input.GetAxis("Mouse Y")* rotateSpeed;


		if (invertY) {
			pivot.Rotate (vertical, 0, 0);
		
		} else {
			pivot.Rotate (-vertical, 0, 0);
		}

		//limit rotation
		if(pivot.rotation.eulerAngles.x > maxAngle && pivot.rotation.eulerAngles.x <180f){

			pivot.rotation = Quaternion.Euler (maxAngle, 0, 0);
		}
		if(pivot.rotation.eulerAngles.x  < 360f + minAngle && pivot.rotation.eulerAngles.x >180f){

			pivot.rotation = Quaternion.Euler (360f+minAngle, 0, 0);
		}

		
		//Move the camera based on the current rotation 
		
		float desiredYAngle =pivot.eulerAngles.y;
		float desiredXAngle =pivot.eulerAngles.x;
		Quaternion rotation = Quaternion.Euler(desiredXAngle,desiredYAngle,0);
		transform.position =target.position-(rotation*offset);
		
		
		if(transform.position.y < target.position.y)
		{
			
			transform.position =new Vector3 (transform.position.x,target.position.y,transform.position.z);
		}
		transform.LookAt(target);
	}
}
