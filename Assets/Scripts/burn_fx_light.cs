using UnityEngine;
using System.Collections;

public class burn_fx_light : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Light>().intensity = Random.Range(0.2f, 5f);
	}
}
