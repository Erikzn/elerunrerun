using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public static GameController instance;
	public PlayerController pc;
	public float speed = 0.02f;
	// private GameObject[] Sections;
	public List<GameObject> Sections;
	public List<GameObject> SectionsInPlay;
	public List<GameObject> Obstacles;

	public bool gameOver = false;

	// Use this for initialization
	void Awake () {
		if(instance == null) {
			instance = this;
			pc = FindObjectOfType<PlayerController>();
		}
		// SectionsInPlay = GameObject.FindGameObjectsWithTag("Section").ToList();
	}

	public void SpawnSection(Vector3 pos, GameObject x) {
		Vector3 SpawnPos = pos + new Vector3(4.88f, 0, 0);
		GameObject s = Sections[Random.Range(0, Sections.Count)];
		GameObject newSection = Instantiate(s, SpawnPos, s.transform.rotation, transform);
		SpawnObstacle(SpawnPos, newSection);
		SectionsInPlay.Add(newSection);
	}

	public void SpawnObstacle (Vector3 pos, GameObject x) {
		int SpawnOrNot = Random.Range(0, 2);
		if(SpawnOrNot == 1) {
			return;
		}
		int index = Random.Range(0, Obstacles.Count);
		GameObject o = Obstacles[index];
		if(x.transform.childCount > 1) {
			int UpOrDown = Random.Range(0, 2);
			if(UpOrDown == 0) {
				Instantiate(o, pos - Vector3.up / 3.3f, o.transform.rotation, x.transform);
			} else {
				Instantiate(o, pos - Vector3.up * 3.8f, o.transform.rotation, x.transform);
			}
		} else {
			Instantiate(o, pos - Vector3.up * 3.8f, o.transform.rotation, x.transform);
		}
	}

	public void GameOver() {
		gameOver = true;
		speed = 0;
		pc.currentParticle.SetActive(false);
		pc.anim.SetTrigger("Dead");
		print("GAME OVER");
	}
	
	// Update is called once per frame
	void Update () {
		if(gameOver) {
			return;
		}
		if (speed > 0.075f) {
			speed = 0.075f;
			return;
		} else {
//			Debug.Log ("Speed: " + speed);
			speed = speed + 0.000005f;
		}
	}
}
