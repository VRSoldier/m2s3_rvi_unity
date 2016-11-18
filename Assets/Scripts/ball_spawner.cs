using UnityEngine;
using System.Collections;

public class ball_spawner : MonoBehaviour {

	public GameObject ball1;
	public GameObject ball2;
	Plane plane;

	// Use this for initialization
	void Start () {
		plane = new Plane(Vector3.left, Vector3.zero); 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float dist = 0f;
			if (plane.Raycast(ray, out dist)) {
				Instantiate(ball1, ray.GetPoint(dist), new Quaternion(0f,0f,0f,0f));
				Instantiate(ball2, ray.GetPoint(dist), new Quaternion(0f,0f,0f,0f));
			}
		}
	}
}
