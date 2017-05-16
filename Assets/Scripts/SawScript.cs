using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour {
	private ParticleSystem particleSystem1;

	void OnEnable () {
		particleSystem1 = GetComponent<ParticleSystem>();
		particleSystem1.Stop();
	}

	void Update () {
		// Continuously rotate saw
		transform.Rotate (0,0,90*Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.GetComponent<Rigidbody2D>()!= null) {
			// If not player disable/hide it
			if(coll.gameObject.tag != "Player") {
				coll.gameObject.SetActive(false);
			}
			// Emit sparks
			particleSystem1.Emit(40);
		}
	}
}
