using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour {


	private Rigidbody thisRigidbody;

	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;
	public float jumpVelocity= 2f;
	private bool holdingjump;
	
	public  float GroundCheckDistance = 1.1f;
    public bool grounded = true;

    private void Start () {
		thisRigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	private void Update ()
    {
		holdingjump = Input.GetButton("Jump");
		CheckGroundStatus();
	}

	void FixedUpdate()
	{
		checkFirstJump();
	}


	void checkFirstJump()
	{
		if (holdingjump)
			thisRigidbody.velocity = new Vector3(thisRigidbody.velocity.x,  jumpVelocity,thisRigidbody.velocity.z);

		if (thisRigidbody.velocity.y < 0 && !grounded && thisRigidbody.velocity.y !=0)
		{ 
			thisRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
		else if (thisRigidbody.velocity.y > 0 && !holdingjump && !grounded && thisRigidbody.velocity.y !=0 )
		{
			thisRigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}

	}
	
	 void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        #if UNITY_EDITOR
            // helper to visualise the ground check ray in the scene view
            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * GroundCheckDistance));
        #endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        grounded = Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, GroundCheckDistance); // true or false
	}
}
