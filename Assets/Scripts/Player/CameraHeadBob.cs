using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeadBob : MonoBehaviour {

	InputSystem inputSystem;

	public bool useHeadbob;
	float speed_Forward, speed_Backward, speed;

	Animator anim;
	PlayerInfo playerInfo;
	PlayerFootsteps footstepSystem;

	[HideInInspector]
	public bool isIdle, isWalkingForward, isWalkingBackward;

	// Use this for initialization
	void Start () {
		//Get Input System
		inputSystem = GameObject.Find("InputSystem").GetComponent<InputSystem>();
		playerInfo = GameObject.Find ("_PlayerSystem").GetComponent<PlayerInfo> ();
		footstepSystem = GameObject.Find ("FootstepSystem").GetComponent<PlayerFootsteps> ();
		anim = GetComponent<Animator> ();
		if (useHeadbob) {
			anim.enabled = true;
		} else {
			anim.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 input = new Vector3 (inputSystem.X_move, 0, inputSystem.Y_move);
		speed_Forward = Mathf.Max (Mathf.Abs(input.x),Mathf.Abs(input.z));
		speed_Backward = Mathf.Max (Mathf.Abs(input.x),Mathf.Abs(input.z)) * 0.5f;
		if (input.z >= 0) {
			speed = Mathf.Max (Mathf.Abs (input.x), Mathf.Abs (input.z));
		} else {
			speed = input.z;
		}
		UpdateAnimator ();
	}

	void UpdateAnimator() {
		anim.SetBool ("Grounded", playerInfo.grounded);
		anim.SetFloat ("Speed", speed);
		anim.SetFloat ("SpeedBackward", speed_Backward);
		anim.SetFloat ("SpeedForward", speed_Forward);

		if (speed > 0) {
			isIdle = false;
			isWalkingForward = true;
			isWalkingBackward = false;
		} else if (speed < 0) {
			isIdle = false;
			isWalkingForward = false;
			isWalkingBackward = true;
		} else {
			isIdle = true;
			isWalkingForward = false;
			isWalkingBackward = false;
		}
	}

	//Called from camera animations
	void PlayerStep(){
		footstepSystem.Step ();
	}
}
