using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	
	public float rotationSpeed = -10f;
	public GameObject cible;

	// Use this for initialization
	void Start () {
		Debug.Log (gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.RotateAround (cible.transform.position, Vector3.up, Time.deltaTime * rotationSpeed);
	}
}
