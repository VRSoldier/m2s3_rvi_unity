using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractionSouris : MonoBehaviour {

	public GameObject text;
	Vector3 lastMouseCoords;
	Collider selectedObject;
	Plane xzPlane;
	Plane screenPlane;
	Vector3 distOriginHitPoint;

	// Use this for initialization
	void Start () {
		text.SetActive(false);
		lastMouseCoords = Vector3.zero;
	}

	void moveXZ() {
		Vector3 mouseDelta = Input.mousePosition - lastMouseCoords;
		Vector3 translation = new Vector3 (mouseDelta.x, 0f, mouseDelta.y);
		selectedObject.transform.position += translation;
		text.GetComponent<Text>().text = selectedObject.name + "\n" + selectedObject.transform.position;
		transform.position += translation;
	}

	void moveScreen() {
		Vector3 mouseCoords = Input.mousePosition;
		
		Ray newMouseRay = Camera.main.ScreenPointToRay (mouseCoords);
		
		float distNewMouseRay;
		if(selectedObject != null && screenPlane.Raycast (newMouseRay, out distNewMouseRay)) {
			this.selectedObject.transform.position = newMouseRay.GetPoint(distNewMouseRay) + distOriginHitPoint;
			text.GetComponent<Text>().text = selectedObject.name + "\n" + selectedObject.transform.position;
			transform.position = newMouseRay.GetPoint(distNewMouseRay * 0.8f);
		}
	}

	//Attention car si on envoie l'objet "à l'horizon", il ira beaucoup trop loin
	void moveScreenXZ() {
		Vector3 mouseCoords = Input.mousePosition;
		
		Ray newMouseRay = Camera.main.ScreenPointToRay (mouseCoords);
		
		float distNewMouseRay;
		if(selectedObject != null && xzPlane.Raycast (newMouseRay, out distNewMouseRay)) {
			this.selectedObject.transform.position = newMouseRay.GetPoint(distNewMouseRay) + distOriginHitPoint;
			text.GetComponent<Text>().text = selectedObject.name + "\n" + selectedObject.transform.position;
			transform.position = newMouseRay.GetPoint(distNewMouseRay * 0.8f);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				if (selectedObject != hit.collider) {
					xzPlane = new Plane (Vector3.up, hit.point);
					screenPlane = new Plane (Camera.main.transform.forward, hit.point);
					selectedObject = hit.collider;
					distOriginHitPoint = hit.collider.transform.position - hit.point;
				}

				text.SetActive (true);
				text.GetComponent<Text> ().text = selectedObject.name + "\n" + selectedObject.transform.position;
				transform.position = ray.GetPoint (hit.distance * 0.8f);

			} else {
					selectedObject = null;
					text.SetActive (false);
			}
			lastMouseCoords = Input.mousePosition;
		} else if (Input.GetMouseButtonUp (0)) {
			selectedObject = null;
		}
		if (Input.GetMouseButton (0)) {
			if(selectedObject != null) 
				moveScreenXZ ();
			lastMouseCoords = Input.mousePosition;
		}
	}

}