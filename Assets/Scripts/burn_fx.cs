using UnityEngine;
using System.Collections;

public class burn_fx : MonoBehaviour
{

		public Light light_system;
		public ParticleSystem particles_system1;
		public ParticleSystem particles_system2;

		// Use this for initialization
		void Start ()
		{
				disable ();
		}

		public bool is_enabled ()
		{
				return light_system.enabled && particles_system1.isPlaying && particles_system2.isPlaying;
		}

		public void enable ()
		{
				light_system.enabled = true;
				particles_system1.Play ();
				particles_system2.Play ();
				GetComponent<AudioSource> ().Play ();
		}

		public void disable ()
		{
				light_system.enabled = false;
				particles_system1.Stop ();
				particles_system2.Stop ();
				GetComponent<AudioSource> ().Stop ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
