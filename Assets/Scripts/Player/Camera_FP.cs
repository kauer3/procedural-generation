using UnityEngine;
using System.Collections;

public class Camera_FP : MonoBehaviour {

	InputSystem inputSystem;

	public Vector2 lookSensitivity = new Vector2 (6, 2);
	public bool clampVerticalRotation;
	float xRot;
	float yRot;

	Transform player;

	public Vector2 yMinMax = new Vector2 (-60, 60);

	public bool smooth;
	public float smoothTime = 0.3f;

	private Quaternion playerTargetRot;
	private Quaternion cameraTargetRot;

	// Use this for initialization
	void Start () {
		player = transform.parent.transform;
		playerTargetRot = player.localRotation;
		cameraTargetRot = transform.localRotation;

		//Get Input System
		inputSystem = GameObject.Find("InputSystem").GetComponent<InputSystem>();
	}

	// Update is called once per frame
	void Update () {
		Vector2 inputDirection = new Vector2 (inputSystem.Y_look, inputSystem.X_look); 

		xRot = inputDirection.x * lookSensitivity.y;
		yRot = inputDirection.y * lookSensitivity.x;

		playerTargetRot *= Quaternion.Euler (0f, yRot, 0f);
		cameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

		if (clampVerticalRotation)
			cameraTargetRot = ClampRotationAroundXAxis (cameraTargetRot);


		if(smooth)
		{
			player.localRotation = Quaternion.Slerp (player.localRotation, playerTargetRot,
				smoothTime * Time.deltaTime);
			transform.localRotation = Quaternion.Slerp (transform.localRotation, cameraTargetRot,
				smoothTime * Time.deltaTime);
		}
		else
		{
			player.localRotation = playerTargetRot;
			transform.localRotation = cameraTargetRot;
		}
	}

	Quaternion ClampRotationAroundXAxis(Quaternion q) {
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

		angleX = Mathf.Clamp (angleX, yMinMax.x, yMinMax.y);

		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}
}
