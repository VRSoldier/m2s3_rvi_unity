using UnityEngine;
using System.Collections;

public class robotic_arm : MonoBehaviour {

	public Animator animator;
	public burn_fx burn_fx;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!burn_fx.is_enabled ()) {
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("burn")) {
				burn_fx.enable ();
			}
		} else {
			if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("burn")) {
				burn_fx.disable ();
			}
		}
 	}
}
