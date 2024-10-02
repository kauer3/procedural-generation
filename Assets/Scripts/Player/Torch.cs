using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

	InputSystem inputSystem;

	Transform torch, torchAnchor;

	Light torchLight;

	public bool lookAtTarget, torchOn;

	float smoothVelocityX, smoothVelocityY;
	public float smoothTime = 1f;
	public float DPadScale = 1f;

	float smoothX, smoothY;
	float smoothTimeAdjust = 0.1f;

	Transform rayCastHitPoint;

	// Use this for initialization
	void Start () {
		//Get Input System
		inputSystem = GameObject.Find("InputSystem").GetComponent<InputSystem>();

		torch = GameObject.Find("Torch").transform;
		torchAnchor = GameObject.Find ("TorchAnchor").transform;
		rayCastHitPoint = GameObject.Find ("RayHitPoint").transform;

		torchLight = torch.GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		float inputX = inputSystem.X_alt;
		float inputY = inputSystem.Y_alt;

		if (inputSystem.Torch)
			ToggleTorch ();

		smoothX = Mathf.SmoothDamp (smoothX, inputX, ref smoothVelocityX, smoothTimeAdjust);
		smoothY = Mathf.SmoothDamp (smoothY, inputY, ref smoothVelocityY, smoothTimeAdjust);

		Vector3 torchTarget = rayCastHitPoint.position;
		torchTarget = torchTarget + transform.right * smoothX * DPadScale;
		torchTarget = torchTarget + transform.up * smoothY * DPadScale;

		if(lookAtTarget)
			torchAnchor.LookAt (torchTarget);
		torch.position = Vector3.Slerp (torch.position, torchAnchor.position,
			smoothTime * Time.deltaTime);

		torch.rotation = Quaternion.Slerp (torch.rotation, torchAnchor.rotation,
			smoothTime * Time.deltaTime);
	}

	public void ToggleTorch () {
		torchOn = !torchOn;
		if (torchOn) {
			torchLight.enabled = true;
		} else {
			torchLight.enabled = false;
		}
	}
}
