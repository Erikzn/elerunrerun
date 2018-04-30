using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SectionController : MonoBehaviour {

	private float speed = 0.05f;
	GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameController.instance;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.left * gc.speed);
		if (transform.localPosition.x < -25f) {
			GameObject last = gc.SectionsInPlay[gc.SectionsInPlay.Count-1];			
			gc.SpawnSection(last.transform.position, last);
			gc.SectionsInPlay.Remove(gameObject);
			Destroy(gameObject);
		}
	}
}
