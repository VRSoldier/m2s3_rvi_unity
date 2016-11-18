using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextureSlider : MonoBehaviour {

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
		target.GetComponent<Renderer>().material.SetFloat ("_Value", slider.value);
	}
}
