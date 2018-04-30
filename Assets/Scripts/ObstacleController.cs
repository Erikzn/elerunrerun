using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

	public PlayerStates colState;

	void Start() {
	}

	// Use this for initialization
	void OnTriggerEnter(Collider col) {
		if(col.tag.Contains("Player")) {
			if(col.GetComponent<PlayerController>().states == colState) {
				transform.GetChild(0).GetComponent<ParticleSystem> ().Play ();
				GetComponent<Renderer> ().enabled = false;
			} else {
				GameController.instance.GameOver();
			}
		}
	}
}
