using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
	Red,
	Blue,
	Green, 
	Yellow
}
public class PlayerController : MonoBehaviour {
	public PlayerStates states;
	private bool Grounded = false;
	private float jumpForce = 1000;
	bool upperFloor = false;
	bool switchingStates;
	public RuntimeAnimatorController redAnim, blueAnim, greenAnim, yellowAnim;
	public Animator anim;
	public GameObject currentParticle;
	public List<GameObject> particles;
	
	string currentAnimTrigger = "";
	float currentAnimTime;

	GUIStyle st = new GUIStyle();
	// Use this for initialization
	void Start () {
		currentParticle = particles[0];
		currentParticle.SetActive(true);
		currentAnimTrigger = "Run";
		st.fontSize = 25;
		st.normal.textColor = Color.red;
		// Time.timeScale = 0.25f;
		// states = PlayerStates.Red;
		anim = GetComponentInChildren<Animator>();
		anim.runtimeAnimatorController = redAnim;
		// SwitchState();
	}

	// Update is called once per frame
	void Update () {
		if(GameController.instance.gameOver) {
			return;
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)) {
			if(!Grounded) {
				return;
			}
			StartCoroutine("Jump", transform.position.y);
		} else if(Input.GetKeyDown(KeyCode.DownArrow )) {
			if(upperFloor) {
				StartCoroutine("GoDown");
				// SET ANIM FOR GOING DOWN
			}
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)) {
			SwitchState();
		}
		if(Input.GetKeyDown(KeyCode.H)) {
			anim.StopPlayback();
			// anim.SetTrigger("Dead");
			// currentAnimTrigger = "Dead";
		}
	}

	void SwitchState() {
		switch (states) {
			case PlayerStates.Red:
				currentParticle.SetActive(false);
				currentParticle = particles[1];
				currentParticle.SetActive(true);
				currentAnimTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
				anim.runtimeAnimatorController = blueAnim;
				anim.Play(currentAnimTrigger, -1, currentAnimTime);
				states = PlayerStates.Blue;
				break;
			case PlayerStates.Blue:
				currentParticle.SetActive(false);
				currentParticle = particles[2];
				currentParticle.SetActive(true);
				currentAnimTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
				anim.runtimeAnimatorController = greenAnim;
				anim.Play(currentAnimTrigger, -1, currentAnimTime);
				states = PlayerStates.Green;
				break;
			case PlayerStates.Green:
				currentParticle.SetActive(false);
				currentParticle = particles[3];
				currentParticle.SetActive(true);
				currentAnimTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
				anim.runtimeAnimatorController = yellowAnim;
				anim.Play(currentAnimTrigger, -1, currentAnimTime);
				states = PlayerStates.Yellow;
				break;
			case PlayerStates.Yellow:
				currentParticle.SetActive(false);
				currentParticle = particles[0];
				currentParticle.SetActive(true);
				anim.runtimeAnimatorController = redAnim;
				anim.Play(currentAnimTrigger, -1, currentAnimTime);
				states = PlayerStates.Red;
				break;
		}
	}

	IEnumerator Jump(float y) {
		gameObject.layer = 10;
		GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpForce);
		Grounded = false;
		currentAnimTrigger = "Jump";
		anim.SetTrigger("Jump");
		yield return new WaitForSeconds(0.25f);
		yield return new WaitForEndOfFrame();
	}

	IEnumerator GoDown() {
		currentAnimTrigger = "Down";
		anim.SetTrigger("Down");
		gameObject.layer = 10;
		yield return new WaitForSeconds(0.25f);
		gameObject.layer = 9;
		yield return new WaitForEndOfFrame();
	}

	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag.Contains("Floor")) {
			if(col.gameObject.tag.Contains("Top")) {
				upperFloor = true;
			} else {
				upperFloor = false;
			}
			Grounded = true;
			if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Run")) {
				currentAnimTrigger = "Run";
				anim.SetTrigger("Run");
			}
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(50, 50, 200, 200), "CurrentCol: " + Grounded, st);
		GUI.Label(new Rect(50, 100, 200, 200), "State: " + states, st);
	}
}
