using UnityEngine;
using System.Collections;

public class NavigationCible : MonoBehaviour {
	
	Vector3 cameraPoint;
	Vector3 targetPoint;
	Vector3 targetPointOnScreen;
	Ray targetRay;
	Vector3 currentPointOnScreen;
	bool dragging = false;
	
	SphereCollider sphereCollider;
	public GameObject head;
	// bookkeep x rotation because Unity uses quaternions and behavior is not adapted to my algorithm
	// no gimbal lock here because body and torso rotations are separated
	Vector3 headEulerAngles;
	
	// Use this for initialization
	void Start ()
	{
		headEulerAngles = head.transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentPointOnScreen = Input.mousePosition;
		
		//initial click
		if (Input.GetMouseButtonDown (0)) {
			targetPointOnScreen = Input.mousePosition;
			targetRay = Camera.main.ScreenPointToRay (targetPointOnScreen);
			RaycastHit hit;
			if (Physics.Raycast (targetRay, out hit)) {
				cameraPoint = Camera.main.transform.position;
				
				//check if not too close to the target point
				if (hit.distance > 1f) {
					//linear interpolation for navigationCible algorithm
					Vector3 hitPoint = targetRay.GetPoint (hit.distance - 1f);
					targetPoint = new Vector3 (hitPoint.x, cameraPoint.y, hitPoint.z);
				} else {
					//if too close, target point = camera point in order not to collide with the target
					targetPoint = this.cameraPoint;
				}
				dragging = true;
			}
		} else if (Input.GetMouseButtonUp (0)) {
			dragging = false;
		}
		
		
		//drag
		if (Input.GetMouseButton (0)) {
			if (dragging) {
				float t = Mathf.Clamp (currentPointOnScreen.y / targetPointOnScreen.y, 0f, 1f);
				this.transform.position = targetPoint * (1f - t) + cameraPoint * t;
				// rotation when dragging
				Ray newRay = Camera.main.ScreenPointToRay(currentPointOnScreen);
				headEulerAngles.y -= computeAngle(targetRay.direction, newRay.direction, this.transform.up);
			}
		} else {
			//rotation when not dragging
			float seuil = 0.3f;
			Vector2 screen_center = new Vector2 (Screen.width, Screen.height) / 2f;
			
			//left-right			
			float lr_rotation = (currentPointOnScreen.x - screen_center.x) / screen_center.x;
			lr_rotation = Mathf.Sign (lr_rotation) * ((Mathf.Max (seuil, Mathf.Abs (lr_rotation))) - seuil) * (1f / (1f - seuil));
			lr_rotation = Mathf.Sign (lr_rotation) * Mathf.Pow (lr_rotation, 2f);
			headEulerAngles.y = headEulerAngles.y + lr_rotation;
			
			//up-down
			float ud_rotation = (currentPointOnScreen.y - screen_center.y) / screen_center.y;
			ud_rotation = Mathf.Sign(ud_rotation) * ((Mathf.Max(seuil, Mathf.Abs(ud_rotation))) - seuil) * (1f / (1f - seuil));
			ud_rotation = Mathf.Sign(ud_rotation) * Mathf.Pow(ud_rotation, 2f);
			
			headEulerAngles.x = Mathf.Clamp(headEulerAngles.x - ud_rotation, -90, 90);
			
		}
		
		head.transform.eulerAngles = headEulerAngles;
	}
	
	public static float computeAngle (Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2 (
			Vector3.Dot (n, Vector3.Cross (v1, v2)),
			Vector3.Dot (v1, v2)) * Mathf.Rad2Deg;
	}
	
}
