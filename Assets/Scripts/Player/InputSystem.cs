using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour {

	public enum ControlType
	{
		controller_PS4,
		mouse
	}

	public float X_move, Y_move, X_look, Y_look, X_alt, Y_alt;
	public bool Interact, Interact_Held, Cancel, Cancel_Held, Menu, Menu_Held, Torch, Torch_Held, Zoom;
	public ControlType controlType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (controlType == ControlType.controller_PS4) {
			//Debug.Log ("Using PS4 Controller");
			X_move = Input.GetAxis ("PS4_Horizontal_L");
			Y_move = Input.GetAxis ("PS4_Vertical_L");
			X_look = Input.GetAxis ("PS4_Horizontal_R");
			Y_look = Input.GetAxis ("PS4_Vertical_R");
			X_alt = Input.GetAxis ("DPad_Horizontal");
			Y_alt = Input.GetAxis ("DPad_Vertical");
			Zoom = Input.GetButton ("R3");
			Interact = Input.GetButtonDown ("X");
			Interact_Held = Input.GetButton ("X");
			Cancel = Input.GetButtonDown ("Circle");
			Cancel_Held = Input.GetButton ("Circle");
			Menu = Input.GetButtonDown ("Options");
			Menu_Held = Input.GetButton ("Options");
			Torch = Input.GetButtonDown ("PadPress");
			Torch_Held = Input.GetButton ("PadPress");
		} else if (controlType == ControlType.mouse) {
			//Debug.Log ("Using Mouse");
			X_move = Input.GetAxis ("Horizontal");
			Y_move = Input.GetAxis ("Vertical");
			X_look = Input.GetAxis ("Mouse X");
			Y_look = Input.GetAxis ("Mouse Y");
			Zoom = Input.GetMouseButton (2);
			Interact = Input.GetMouseButtonUp (0);
			Interact_Held = Input.GetMouseButton (0);
			Cancel = Input.GetMouseButtonUp (1);
			Cancel_Held = Input.GetMouseButton (1);
			Menu = Input.GetKeyDown (KeyCode.Escape);
			Menu_Held = Input.GetKey (KeyCode.Escape);
			Torch = Input.GetKeyDown (KeyCode.Tab);
			Torch_Held = Input.GetKey (KeyCode.Tab);
		}

		if (Cancel)
			Debug.Log ("Right Mouse Click");

		if (Cancel_Held)
			Debug.Log ("Right Mouse Held");
	}
}
