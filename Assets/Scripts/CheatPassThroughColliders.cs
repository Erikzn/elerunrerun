using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatPassThroughColliders : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if(col.tag != "Player") {
			return;
		}
		col.gameObject.layer = 9;
	}
}
