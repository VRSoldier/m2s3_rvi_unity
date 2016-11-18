using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VertexSlider : MonoBehaviour {
	
	Slider slider;
	public GameObject target;
	
	// Use this for initialization
	void Start () {
		slider = GameObject.Find ("Slider").GetComponent<Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		foreach (Transform child_t in target.transform) {
			GameObject child = child_t.gameObject;
			if(child.name != "Bip01") {
				child.GetComponent<Renderer>().material.SetFloat ("_Value", slider.value);
			}
		}
	}
}
