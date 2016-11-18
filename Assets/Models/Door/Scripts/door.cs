using UnityEngine;
using System.Collections;

public class door : MonoBehaviour
{

		bool opened = false;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!this.GetComponent<Animation>().isPlaying) {
						if (!this.opened && Input.GetMouseButtonDown (0)) {
								this.GetComponent<Animation>().Play ("open_door");
								this.opened = true;
						} else if (this.opened && Input.GetMouseButtonDown (1)) {
								this.GetComponent<Animation>().Play ("close_door");
								this.opened = false;
						}
				}
		}
}
