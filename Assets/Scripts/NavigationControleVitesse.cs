using UnityEngine;
using System.Collections;

public class NavigationControleVitesse : MonoBehaviour {
	
	float translateSpeed = 0.01f;
	float maxTranslateSpeed = 7f;

	float rotateSpeed = 0.01f;
	float maxRotateSpeed = 7f;

	public GameObject head;
	Vector3 clickedPosition;
	int lastButtonClicked;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			clickedPosition = Input.mousePosition;
			lastButtonClicked = 0;
		}
		if (Input.GetMouseButtonDown (1)) {
			clickedPosition = Input.mousePosition;
			lastButtonClicked = 1;
		}
		
		if (Input.GetMouseButton (0) && lastButtonClicked == 0) {
			Vector3 newPosition = Input.mousePosition;
			Vector3 delta = newPosition - clickedPosition;
			//y axis
			float zSpeed = Mathf.Clamp(delta.y * translateSpeed, -maxTranslateSpeed, maxTranslateSpeed);
			this.transform.Translate(new Vector3(0f, 0f, zSpeed));
			
			//x axis
			float xSpeed = Mathf.Clamp(delta.x * translateSpeed, -maxTranslateSpeed, maxTranslateSpeed);
			this.transform.Translate(new Vector3(xSpeed, 0f, 0f));
		}

		if (Input.GetMouseButton (1) && lastButtonClicked == 1) {
			Vector3 newPosition = Input.mousePosition;
			Vector3 delta = newPosition - clickedPosition;
			//y axis
			float ySpeed = Mathf.Clamp(delta.y * rotateSpeed, -maxRotateSpeed, maxRotateSpeed);
			float newRotation = head.transform.eulerAngles.x - ySpeed;
			if (newRotation < 90f || newRotation > 270f) {
				head.transform.RotateAround(this.transform.position, this.transform.right, -ySpeed);
			}
			//x axis
			float xSpeed = Mathf.Clamp(delta.x * rotateSpeed, -maxRotateSpeed, maxRotateSpeed);
			this.transform.RotateAround(this.transform.position, Vector3.up, xSpeed);
		}

	}
}
