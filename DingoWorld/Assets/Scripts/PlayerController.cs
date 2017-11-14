using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;
	public float SecondJumpForce;
	public float ThirdJumpForce;
	public float walljumpForce;
	public float jumpwindowTime;
	public float jumptimer;

	public CharacterController controller;
	
	private Vector3 moveDirection;
	public float gravityScale;
	private float forceY;
	private float invertGrav; 
	public float gravity = 20.0F;
	public float airTime = 2f;
	private bool landed;
	private bool recentlyJumped;
	public int nJump;
	
    private Transform camTransform;
	
	
	public Transform pivot;
	public float rotateSpeed;
	public GameObject playerModel;
	public Animator anim;

	public float walljumpSpeed;
	public bool isWalljumping;
	public bool isOnWall;
	public float wallFallSpeed;
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
		if (!isWalljumping && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Falling Flat Impact")) {
			moveDirection = (transform.forward * Input.GetAxis ("Vertical")) + (transform.right * Input.GetAxis ("Horizontal"));
			moveDirection = moveDirection.normalized * moveSpeed;
		}
		moveDirection.y=yAux;
		if(controller.isGrounded){
			moveDirection.y=0f;
			isWalljumping = false;
			forceY = 0f;
			invertGrav = gravity * airTime;
			if (Input.GetButtonDown("Jump")){
					


				if ( (Mathf.Abs (controller.velocity.x) == 0 && Mathf.Abs (controller.velocity.z) == 0)) {
					nJump = 1;
				} 
			


				if (!landed)
					nJump = 0;
				
				recentlyJumped = true;
				jumptimer = jumpwindowTime;

				if (nJump != 3 && landed) {
					nJump++;
				}
				else {
					nJump = 1;
				}
					

				//moveDirection.y=jumpForce;
				switch (nJump){
				case 1:
					forceY = jumpForce;
					break;
				case 2:
					forceY = SecondJumpForce;
					break;
				case 3:
					forceY = ThirdJumpForce;
					break;
				}
			}

			anim.SetBool ("Fall", false);
			//anim.SetBool ("OnWall", false);
			isOnWall = false;
			anim.SetBool ("WallJump", false);
		}

		//hold jump
		if(Input.GetKey(KeyCode.Space) && forceY != 0 && nJump==1){
			invertGrav -= Time.deltaTime;
			forceY += invertGrav*Time.deltaTime;
		} 

		//moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
		/*if (!isOnWall)
			moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
		else {
				moveDirection.y = moveDirection.y + (Physics.gravity.y * (gravityScale / 8) * Time.deltaTime);
			if (Mathf.Abs (controller.velocity.y) > wallFallSpeed) {
				moveDirection.y = (Physics.gravity.y * (gravityScale / 4) * Time.deltaTime);
			}
		}*/


		forceY -= gravity*Time.deltaTime* gravityScale;
		moveDirection.y = forceY;

		controller.Move(moveDirection*Time.deltaTime);
		
		//Rotate to camera
		if(Input.GetAxis("Vertical")!=0 || Input.GetAxis("Horizontal")!=0){
			
			transform.rotation=Quaternion.Euler(0f,pivot.rotation.eulerAngles.y,0f);
			Quaternion newRotation=Quaternion.LookRotation(new Vector3(moveDirection.x,0f,moveDirection.z));
			playerModel.transform.rotation=Quaternion.Slerp(playerModel.transform.rotation,newRotation,rotateSpeed *Time.deltaTime);
			
		}


		anim.SetBool ("Grounded", controller.isGrounded);
		anim.SetFloat ("Speed", Mathf.Abs(Input.GetAxis("Vertical"))+  Mathf.Abs(Input.GetAxis("Horizontal")) );



		anim.SetBool ("OnWall", isOnWall);

		anim.SetInteger ("Jump", nJump);



		if (recentlyJumped) {

			if (controller.isGrounded) {
				landed = true;
				recentlyJumped = false;

			}


		
		}


		if (landed) {

			jumptimer -= Time.deltaTime;
			if (jumptimer < 0) {
				landed = false;

			}



		}

	}
	
	private void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if(!controller.isGrounded && hit.normal.y < 0.1f)
		{
			transform.rotation=Quaternion.Euler(0f,hit.transform.rotation.eulerAngles.y,0f);
			Quaternion newRotation=Quaternion.LookRotation(new Vector3(moveDirection.x,0f,moveDirection.z));
			playerModel.transform.rotation=Quaternion.Lerp(playerModel.transform.rotation,newRotation,rotateSpeed *Time.deltaTime);
			isOnWall=true;

			anim.SetBool ("WallJump", false);
			if(Input.GetButtonDown("Jump"))
			{
				anim.SetBool ("WallJump", true);
				isOnWall=false;
				isWalljumping = true;
				Debug.DrawRay (hit.point, hit.normal, Color.red, 1.25f);
				moveDirection = hit.normal * walljumpSpeed;


				forceY = walljumpForce;


			}
		}


	}

	void OnCollisionExit(Collision collisionInfo) {
		Debug.Log("No longer in contact with " + collisionInfo.transform.name);
	}

}
