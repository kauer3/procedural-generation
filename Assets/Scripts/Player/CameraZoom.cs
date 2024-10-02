using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

	InputSystem inputSystem;

	float defaultFOV;
	[Range(-50,50)]
	public float zoomAmount = 15f;
	[Range(0,5)]
	public float smoothTime = 0.5f;
	[HideInInspector]
	public bool zoomed;
	float smoothVelocity;

	Camera cam;

	// Use this for initialization
	void Start () {
		//Get Input System
		inputSystem = GameObject.Find("InputSystem").GetComponent<InputSystem>();

		cam = GetComponent<Camera> ();
		defaultFOV = cam.fieldOfView;
	}
	
	// Update is called once per frame
	void Update () {
		float fov;
		if (inputSystem.Zoom && inputSystem.X_move == 0 && inputSystem.Y_move == 0) {
			fov = defaultFOV - zoomAmount;
			zoomed = true;
		} else {
			fov = defaultFOV;
			zoomed = false;
		}

		cam.fieldOfView = Mathf.SmoothDamp (cam.fieldOfView, fov, ref smoothVelocity, smoothTime);
	}
}
