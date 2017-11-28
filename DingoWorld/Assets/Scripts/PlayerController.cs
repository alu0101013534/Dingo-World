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
	private bool ps4Controller = false;

	private Transform camTransform;


	public Transform pivot;
	public float rotateSpeed;
	public GameObject playerModel;
	public Animator anim;

	public float walljumpSpeed;
	public bool isWalljumping;
	public bool isOnWall;
	public float wallFallSpeed;
	private bool isDead;
	public float deathtimer;
	private Vector3 respawnpoint;
	public int lives=3;
	public bool isDiamondCollected;
	private float diamondTimer =3f;
	private bool isDeathFall;
	private GameObject killFloor;
	private bool isGameOver;

	private PlayerHealth ph;
	public bool spawned;


	// Use this for initialization
	void Start () {
		//thisRigidbody= GetComponent<Rigidbody>();
		controller=GetComponent<CharacterController>();
		respawnpoint = transform.position;


	
		lives = PlayerPrefs.GetInt ("Lifes",3);
		killFloor = GameObject.FindGameObjectWithTag ("KillingFloor");

		ph = gameObject.GetComponent<PlayerHealth> ();

	}

	// Update is called once per frame
	void Update () {
		//thisRigidbody.velocity=new Vector3(Input.GetAxis("Horizontal")*moveSpeed, thisRigidbody.velocity.y,Input.GetAxis("Vertical")*moveSpeed);

		string[] names = Input.GetJoystickNames();
		for(int x = 0; x < names.Length; x++)
		{
			if (names[x].Length == 19)
			{
				ps4Controller = true;
				break;
			}
		}


		//moveDirection=new Vector3(Input.GetAxis("Horizontal")*moveSpeed, moveDirection.y,Input.GetAxis("Vertical")*moveSpeed);
		float yAux=moveDirection.y;
		if (!isWalljumping && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Falling Flat Impact")) {
			if (ps4Controller)
			{
				moveDirection = (transform.forward * Input.GetAxis("PS4_LeftAnalogVertical")) + (transform.right * Input.GetAxis("PS4_LeftAnalogHorizontal"));

			}
			else
			{
				moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));

			}
			moveDirection = moveDirection.normalized * moveSpeed;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Falling Flat Impact") || isDead || isDiamondCollected) {
			moveDirection = new Vector3 (0, 0, 0);
		}
		moveDirection.y=yAux;
		if(controller.isGrounded){
			moveDirection.y=0f;
			isWalljumping = false;
			forceY = 0f;
			invertGrav = gravity * airTime;
			if ((Input.GetButtonDown("PS4_X") || Input.GetButtonDown("Jump") )&& !isDiamondCollected){



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
		if((Input.GetButton("PS4_X") || Input.GetKey(KeyCode.Space)) && forceY != 0 && nJump==1){
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
		if(!isDiamondCollected &&(Input.GetAxis("PS4_LeftAnalogVertical")!=0 || Input.GetAxis("PS4_LeftAnalogHorizontal")!=0 || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0))
		{

			transform.rotation=Quaternion.Euler(0f,pivot.rotation.eulerAngles.y,0f);
			Quaternion newRotation=Quaternion.LookRotation(new Vector3(moveDirection.x,0f,moveDirection.z));
			playerModel.transform.rotation=Quaternion.Slerp(playerModel.transform.rotation,newRotation,rotateSpeed *Time.deltaTime);

		}


		anim.SetBool ("Grounded", controller.isGrounded);
		anim.SetFloat ("Speed", Mathf.Abs(controller.velocity.x)+  Mathf.Abs(controller.velocity.z));



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
		if (isDiamondCollected) {

			diamondTimer -= Time.deltaTime;
			if (diamondTimer < 0) {
				isDiamondCollected = false;
				diamondTimer = 3f;

			}

		}


		if (isDead && lives >= 0) {
			deathtimer -= Time.deltaTime;
			if (deathtimer < 0) {
				if (!isGameOver) {
					Respawn ();
				} else {
					GameObject.FindObjectOfType<GameManager> ().GameOver ();
				}
				deathtimer = 3f;

			}
		}


		if (!isDeathFall &&transform.position.y <= killFloor.transform.position.y) {
			isDeathFall = true;
			ph.DamagePlayer (0);
			deathtimer = 1f;
			death ();
		}


		if (ph.invicibilityCounter <= 0) {
		
			spawned = false;
		}

		anim.SetBool ("Death", isDead);
		anim.SetBool ("DeathFall", isDeathFall);
		anim.SetBool ("DiamondCollected", isDiamondCollected);

		ps4Controller = false;
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
			if(Input.GetButtonDown("PS4_X") || Input.GetButtonDown("Jump"))
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

	public void death(){
		isDead = true;

		if(!isGameOver && lives > 0)
			lives--;

		if (lives <= 0) {
			isGameOver = true;
		} 




		//wait respawn 
	}
	public void addLive(){
		lives++;
	}

	private void Respawn(){

		isDead = false;
		isDeathFall = false;

		spawned = true;
		transform.position = respawnpoint;


		gameObject.GetComponent<PlayerHealth> ().HealPlayer(1);
	}


	public void DiamondCollected(){

		isDiamondCollected = true;
	}
}