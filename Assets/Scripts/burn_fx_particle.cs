using UnityEngine;
using System.Collections;

public class burn_fx_particle : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
				ParticleSystem.EmissionModule em = this.GetComponent<ParticleSystem> ().emission;
				em.enabled = true;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
