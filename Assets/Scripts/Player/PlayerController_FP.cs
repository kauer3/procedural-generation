using UnityEngine;
using System.Collections;

public class PlayerController_FP : MonoBehaviour {

	InputSystem inputSystem;

	float inputX, inputY;
	float walkSpeed = 3f;
	public float walkSpeedForward = 3f;
	public float walkSpeedBackward = 1f;
	public bool normalizeMotion;
	public bool avoidWalls;

	float distF, distB, distL, distR;
	public float stoppingDistance = 0.6f;

	Transform rayCastHitPoint;

	public bool showDebug;

	[HideInInspector]
	public bool grounded;

	void Start() {
		//Get Input System
		inputSystem = GameObject.Find("InputSystem").GetComponent<InputSystem>();
	}

	// Update is called once per frame
	void Update () {
		inputX = inputSystem.X_move;
		inputY = inputSystem.Y_move;

		GroundCheck ();
		if(avoidWalls)
			AvoidWalls ();
		MovePlayer ();
	}

	void GroundCheck(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -transform.up, out hit, transform.localScale.y+ 0.05f)) {
			grounded = true;
		} else {
			grounded = false;
		}
	}

	void MovePlayer(){
		Vector3 input = new Vector3 (inputX, 0, inputY);
		Vector3 inputDirection = input.normalized;

		if (input.z >= 0) {
			walkSpeed = walkSpeedForward;
		} else {
			walkSpeed = walkSpeedBackward;
		}

		if (normalizeMotion) {
			float mag = Mathf.Max (Mathf.Abs(input.x),Mathf.Abs(input.z));
			Vector3 motion = inputDirection * mag;
			transform.Translate (motion * walkSpeed * Time.deltaTime);
		} else {
			transform.Translate (input * walkSpeed * Time.deltaTime);
		}
	}

	void AvoidWalls(){
		RaycastHit hitF, hitB, hitL, hitR;

		//Forward Ray
		if (Physics.Raycast (transform.position, transform.forward, out hitF, stoppingDistance + 1)) {
			distF = Vector3.Distance (transform.position, hitF.point);
		} else {
			distF = stoppingDistance + 1;
		}

		//Backward Ray
		if (Physics.Raycast (transform.position, -transform.forward, out hitB, stoppingDistance + 1)) {
			distB = Vector3.Distance (transform.position, hitB.point);
		} else {
			distB = stoppingDistance + 1;
		}

		//Left Ray
		if (Physics.Raycast (transform.position, -transform.right, out hitL, stoppingDistance + 1)) {
			distL = Vector3.Distance (transform.position, hitL.point);
		} else {
			distL = stoppingDistance + 1;
		}

		//Right Ray
		if (Physics.Raycast (transform.position, transform.right, out hitR, stoppingDistance + 1)) {
			distR = Vector3.Distance (transform.position, hitR.point);
		} else {
			distR = stoppingDistance + 1;
		}

		//Cancel Forward Input
		if (distF < stoppingDistance) {
			if(inputY > 0)
				inputY = 0;
		}

		//Cancel Backward Input
		if (distB < stoppingDistance) {
			if(inputY < 0)
				inputY = 0;
		}

		//Cancel Left Input
		if (distL < stoppingDistance) {
			if(inputX < 0)
				inputX = 0;
		}

		//Cancel Right Input
		if (distR < stoppingDistance) {
			if(inputX > 0)
				inputX = 0;
		}
	}

	void OnDrawGizmos(){
		if (showDebug) {
			if (distF < stoppingDistance) {
				Gizmos.color = Color.red;
			} else {
				Gizmos.color = Color.green;
			}
			Gizmos.DrawRay (transform.position, transform.forward * stoppingDistance);

			if (distB < stoppingDistance) {
				Gizmos.color = Color.red;
			} else {
				Gizmos.color = Color.green;
			}
			Gizmos.DrawRay (transform.position, -transform.forward * stoppingDistance);

			if (distL < stoppingDistance) {
				Gizmos.color = Color.red;
			} else {
				Gizmos.color = Color.green;
			}
			Gizmos.DrawRay (transform.position, -transform.right * stoppingDistance);

			if (distR < stoppingDistance) {
				Gizmos.color = Color.red;
			} else {
				Gizmos.color = Color.green;
			}
			Gizmos.DrawRay (transform.position, transform.right * stoppingDistance);
		}
	}
}