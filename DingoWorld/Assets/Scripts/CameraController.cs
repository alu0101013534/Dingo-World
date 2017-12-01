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


	public float maxAngle;
	public float minAngle;
	private PlayerController pc;

    private int invX;
    private int invY;

    public void UpdatePrefs()
    {
        invX = PlayerPrefs.GetInt("InvertX");
        invY = PlayerPrefs.GetInt("InvertY");
        rotateSpeed = PlayerPrefs.GetFloat("Sensibilidad");
    }


	private void Start () {
		if(!useOffsetValues)
		{
			offset = target.position - transform.position;
		}

		pivot.transform.position = target.transform.position;
		pivot.transform.parent = null;

		Cursor.lockState = CursorLockMode.Locked;
		pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        invX = PlayerPrefs.GetInt("InvertX");
        invY = PlayerPrefs.GetInt("InvertY");
        rotateSpeed = PlayerPrefs.GetFloat("Sensibilidad");
    }

    private void LateUpdate () {

		if (!pc.isDiamondCollected) {
			string[] names = Input.GetJoystickNames();
			for (int x = 0; x < names.Length; x++) {
				if (names [x].Length == 19) {
					ps4Controller = true;
					break;
				}
			}

			pivot.transform.position = target.transform.position;

			float horizontal = Input.GetAxis (ps4Controller ? "PS4_RightAnalogHorizontal" : "Mouse X") * rotateSpeed;

			pivot.transform.position = target.transform.position;

            pivot.Rotate(0, (invX == 1) ? +horizontal : -horizontal, 0);
            
			// get y position of the mouse and rotate
			float vertical = Input.GetAxis (ps4Controller ? "PS4_RightAnalogVertical" : "Mouse Y") * rotateSpeed;

            // get y position of the mouse and rotate
            pivot.Rotate((invY == 1) ? +vertical : -vertical, 0, 0);

			//limit rotation
			if (pivot.rotation.eulerAngles.x > maxAngle && pivot.rotation.eulerAngles.x < 180f) {

				pivot.rotation = Quaternion.Euler(maxAngle, 0, 0);
			}
			if (pivot.rotation.eulerAngles.x < 360f + minAngle && pivot.rotation.eulerAngles.x > 180f) {

				pivot.rotation = Quaternion.Euler(360f + minAngle, 0, 0);
			}


			//Move the camera based on the current rotation 

			float desiredYAngle = pivot.eulerAngles.y;
			float desiredXAngle = pivot.eulerAngles.x;
			Quaternion rotation = Quaternion.Euler (desiredXAngle, desiredYAngle, 0);
			transform.position = target.position - (rotation * offset);


			if (transform.position.y < target.position.y)
            {
				transform.position = new Vector3 (transform.position.x, target.position.y, transform.position.z);
			}

			transform.LookAt (target);
			ps4Controller = false;
		}
	}

}