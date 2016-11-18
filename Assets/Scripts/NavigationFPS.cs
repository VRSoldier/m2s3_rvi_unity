using UnityEngine;
using System.Collections;

public class NavigationFPS : MonoBehaviour {

	public GameObject head;
	float walkSpeed;
	float rotationSpeed;
	Vector3 oldMousePos;

	// Use this for initialization
	void Start () {
		walkSpeed = 1f;
		rotationSpeed = 0.5f;
		oldMousePos = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newMousePos = Input.mousePosition;
		Vector3 mouseDelta = newMousePos - oldMousePos;
		
		this.transform.RotateAround (this.transform.position, Vector3.up, mouseDelta.x * rotationSpeed);


		float newRotation = head.transform.eulerAngles.x - mouseDelta.y * rotationSpeed;
		if (newRotation < 90f || newRotation > 270f) {
			head.transform.RotateAround (this.transform.position, this.transform.right, - mouseDelta.y * rotationSpeed);
		}

		Vector3 translation = Vector3.zero;
		if (Input.GetKey (KeyCode.Z) || Input.GetKey (KeyCode.UpArrow)) {
			translation += Vector3.forward;
		}
		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
			translation += Vector3.back;
		}
		if (Input.GetKey (KeyCode.Q) || Input.GetKey (KeyCode.LeftArrow)) {
			translation += Vector3.left;
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			translation += Vector3.right;
		}
		this.transform.Translate( translation * walkSpeed);

		oldMousePos = newMousePos;
	}
}
