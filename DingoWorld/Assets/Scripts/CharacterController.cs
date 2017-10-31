using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {


	public float moveSpeed = 5.0f;
    public float drag = 0.5f;
    public float terminalRotationSpeed = 100025.0f;
    public Vector3 MoveVector { set; get; }
	
	
    private Rigidbody thisRigidbody;
	
	
	
    private Transform camTransform;
	void Start () {
		
        thisRigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
        MoveVector = PoolInput();


        // rotate moveVector
        MoveVector = RotateWithView();
        //move
        Move();

		
		
	}
	
	 private Vector3 RotateWithView()
    {
        
        if (camTransform != null)
        {
            Vector3 dir = camTransform.TransformDirection(MoveVector);
            dir.Set(dir.x, 0, dir.z);
            return dir.normalized*MoveVector.magnitude;
        }
        else
        {
            camTransform = Camera.main.transform;
            return MoveVector;
        }


    }
	
	private void Move() {
          if(thisRigidbody.velocity.x!=0f && thisRigidbody.velocity.z != 0f)
           transform.rotation = Quaternion.LookRotation(new Vector3(thisRigidbody.velocity.x, 0, thisRigidbody.velocity.z));

       thisRigidbody.AddForce((MoveVector * moveSpeed));
     
    }
	
	private Vector3 PoolInput()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        if (dir.magnitude > 1)
            dir.Normalize();

        return dir;
    }
	
	
	
}
