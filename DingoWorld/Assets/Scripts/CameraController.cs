using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	
	public bool useOffsetValues;
	public float rotateSpeed;
    public bool ps4Controller = false;

    public Transform pivot;

	// Use this for initialization
	void Start () {
		if(!useOffsetValues)
		{
			offset=target.position-transform.position;
		}
		
		pivot.transform.position=target.transform.position;
		//pivot.transform.parent=target.transform;
		
		pivot.transform.parent=null;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            if (names[x].Length == 19)
            {
                ps4Controller = true;
                break;
            }
        }

        if (ps4Controller) { rotateSpeed = 30f; }else { rotateSpeed = 5f; }

        pivot.transform.position=target.transform.position;
        //get x position of the mouse and rotate
        float horizontal;
        if (ps4Controller)
        {
            horizontal = Input.GetAxis("PS4_RightAnalogHorizontal") * rotateSpeed;
        }
        else
        {
            horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        }
		
		pivot.Rotate(0,horizontal,0);
        //get y position of the mouse and rotate

        float vertical;
        if (ps4Controller)
        {
            vertical = Input.GetAxis("PS4_RightAnalogVertical") * rotateSpeed;
        }
        else
        {
            vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        }
        
		pivot.Rotate(-vertical,0,0);
		
		
		
		
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
        ps4Controller = false;
    }

    
}
