using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

	//Player
	public GameObject player;
	public GameObject head;
	public GameObject eyes;

	PlayerController_FP playerController;
//	Rigidbody rb;
	PlayerRaycast playerRaycast;
//	InputSystem inputSystem;

	//Camera
//	Camera cam;
	Camera_FP cam_FP;
	CameraHeadBob headBob;
	CameraZoom camZoom;

	//Is the PlayerRaycast hitting something?
	public bool hasTarget;
	//Raycast targets name and tag
	public string targetName, targetTag;
	//Distance to target from rayStart
	public float targetDistance;

	//Players Motion State (based on camera head bob animation)
	public bool isIdle, isWalkingForward, isWalkingBackward;
	//Is the player standing on the ground (not falling or in the air)?
	public bool grounded;
	//Is the camera zoomed in?
	public bool zoomed;

	public bool showAllDebug, hideAllDebug;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		head = GameObject.Find ("Player_Head");
		eyes = GameObject.Find ("Player_Eyes");

		//Get Input System
//		inputSystem = GameObject.Find("InputSystem").GetComponent<InputSystem>();

		//Get Player Scripts
		playerController = player.GetComponent<PlayerController_FP> ();
//		rb = player.GetComponent<Rigidbody> ();

		//Get Head Scripts
		playerRaycast = head.GetComponent<PlayerRaycast> ();

		//Get Eyes scripts
//		cam = eyes.GetComponent<Camera> ();
		headBob = eyes.GetComponent<CameraHeadBob> ();
		camZoom = eyes.GetComponent<CameraZoom> ();
	}
	
	// Update is called once per frame
	void Update () {
		SetInfo ();
		GetInfo ();
	}

	void GetInfo(){
		hasTarget = playerRaycast.hitSomething;
		targetName = playerRaycast.targetName;
		targetTag = playerRaycast.targetTag;
		targetDistance = playerRaycast.targetDist;
		isIdle = headBob.isIdle;
		isWalkingForward = headBob.isWalkingForward;
		isWalkingBackward = headBob.isWalkingBackward;
		zoomed = camZoom.zoomed;
		grounded = playerController.grounded;
	}

	void SetInfo(){
		if (showAllDebug) {
			playerRaycast.showDebug = true;
			playerController.showDebug = true;
		} else {
			playerRaycast.showDebug = false;
			playerController.showDebug = false;
		}
	}
}
