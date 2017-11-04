using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

public float moveSpeed;
//public Rigidbody thisRigidbody;
public float jumpForce;

public CharacterController controller;
	
	private Vector3 moveDirection;
	public float gravityScale;
	
	
    private Transform camTransform;
	
	
	public Transform pivot;
	public float rotateSpeed;
	public GameObject playerModel;
	public Animator anim;
	// Use this for initialization
	void Start () {
		//thisRigidbody= GetComponent<Rigidbody>();
		controller=GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		//thisRigidbody.velocity=new Vector3(Input.GetAxis("Horizontal")*moveSpeed, thisRigidbody.velocity.y,Input.GetAxis("Vertical")*moveSpeed);
		
				
		//moveDirection=new Vector3(Input.GetAxis("Horizontal")*moveSpeed, moveDirection.y,Input.GetAxis("Vertical")*moveSpeed);
		float yAux=moveDirection.y;
		moveDirection= (transform.forward *Input.GetAxis("Vertical") )+(transform.right *Input.GetAxis("Horizontal") );
		moveDirection=moveDirection.normalized*moveSpeed;
		moveDirection.y=yAux;
		if(controller.isGrounded){
			moveDirection.y=0f;
			if (Input.GetButtonDown("Jump")){
				
				moveDirection.y=jumpForce;
			}

			anim.SetBool ("Fall", false);
		}
		
		moveDirection.y=moveDirection.y +(Physics.gravity.y*gravityScale*Time.deltaTime);
		controller.Move(moveDirection*Time.deltaTime);
		
		
		if(Input.GetAxis("Vertical")!=0 || Input.GetAxis("Horizontal")!=0){
			
			transform.rotation=Quaternion.Euler(0f,pivot.rotation.eulerAngles.y,0f);
			Quaternion newRotation=Quaternion.LookRotation(new Vector3(moveDirection.x,0f,moveDirection.z));
			playerModel.transform.rotation=Quaternion.Slerp(playerModel.transform.rotation,newRotation,rotateSpeed *Time.deltaTime);
			
		}


		anim.SetBool ("Grounded", controller.isGrounded);
		anim.SetFloat ("Speed", Mathf.Abs(Input.GetAxis("Vertical"))+  Mathf.Abs(Input.GetAxis("Horizontal")) );
		
	}
	
	

}
