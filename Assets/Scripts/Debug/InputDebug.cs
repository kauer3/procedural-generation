using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputDebug : MonoBehaviour {

	Canvas canvas;
	Text[] text;
	public Color positiveColour, negativeColour;
	bool showDebug;

	// Use this for initialization
	void Start () {
		canvas = GetComponentInChildren<Canvas> ();
		text = canvas.GetComponentsInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown("PadPress"))
			showDebug = !showDebug;

		if (showDebug) {
			canvas.gameObject.SetActive (true);
		} else {
			canvas.gameObject.SetActive (false);
		}
		
		for (int i = 0; i < text.Length; i++) {
			
			if (text [i].name != "Input Debug") {
				//Set Debug Text Colour Based on Input Value
				if (Input.GetAxisRaw (text [i].name) > 0) {
					text [i].color = positiveColour;
				} else if (Input.GetAxisRaw (text [i].name) < 0) {
					text [i].color = negativeColour;
				} else {
					text [i].color = Color.white;
				}
				//Update Debug Text
				text [i].text = text [i].name + " = " + Input.GetAxisRaw (text [i].name);
			}
			//Debug.Log(text[i].name);
		}
	}
}
