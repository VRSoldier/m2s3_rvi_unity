using UnityEngine;
using System.Collections;

public class soldier : MonoBehaviour {

	double idleTime = 0d;

	public double idleTimeBeforeRelaxed = 1d;
	public int forward_speed = 3;
	public int backward_speed = 2;

	public int sprint_multiplier = 2;

	// Use this for initialization
	void Start () {
		this.GetComponent<Animation>().CrossFade("soldierIdleRelaxed");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 translation = Vector3.zero;
		int rotation = 0;
		int speed = 0;

		//Handle inputs
		if (Input.GetKey (KeyCode.UpArrow)) {
			translation.z += 1;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			translation.z -= 1;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			rotation -= 100;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			rotation += 100;
		}

		//Handle Movements
		if (translation.z > 0) {
			speed = forward_speed;
		} else {
			speed = backward_speed;
		}
		if (Input.GetKey (KeyCode.LeftShift)) {
			speed *= sprint_multiplier;
		}

		transform.Translate(translation * Time.deltaTime * speed);
		transform.Rotate (Vector3.up, (rotation * Time.deltaTime));

		//Handle Animations
		if (translation.z == 0) {
			if (rotation > 0) {
				GetComponent<Animation>().CrossFade ("soldierSpinRight");
			} else if (rotation < 0) {
				GetComponent<Animation>().CrossFade ("soldierSpinLeft");
			} else {
				if (this.idleTime > this.idleTimeBeforeRelaxed) {
					GetComponent<Animation>().CrossFade ("soldierIdleRelaxed");
				} else {
					GetComponent<Animation>().CrossFade ("soldierIdle");
				}
			}
		} if (translation.z > 0) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				GetComponent<Animation>() ["soldierSprint"].speed = 1;
				GetComponent<Animation>().CrossFade ("soldierSprint");
			} else {
				GetComponent<Animation>() ["soldierRun"].speed = 1;
				GetComponent<Animation>().CrossFade ("soldierRun");
			}
		} else if (translation.z < 0) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				GetComponent<Animation>() ["soldierRun"].speed = -1;
				GetComponent<Animation>().CrossFade ("soldierRun");
			} else {
				GetComponent<Animation>() ["soldierWalk"].speed = -1;
				GetComponent<Animation>().CrossFade ("soldierWalk");
			}
		}

		if (GetComponent<Animation>().IsPlaying("soldierIdle") || GetComponent<Animation>().IsPlaying("soldierIdleRelaxed")) {
			this.idleTime += Time.deltaTime;
		} else {
			this.idleTime = 0d;
		}
	}
}
