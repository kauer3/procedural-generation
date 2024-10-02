using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class F_Room : MonoBehaviour {

	public bool onPath, isEntrance, isExit;
	public int gridX, gridY;
	public List<F_Room> neighbours;
	public List<string> openings = new List<string> ();

	TextMeshPro indexLabel;

	public GameObject[] bases;

	void Awake(){
		indexLabel = GetComponentInChildren<TextMeshPro> ();
	}

	public void UpdateInfo(){
		indexLabel.text = ("[" + gridX + "," + gridY + "]");
		if (onPath) {
			indexLabel.color = Color.yellow;
		}
		if (isEntrance) {
			indexLabel.color = Color.green;
		}
		if (isExit) {
			indexLabel.color = Color.red;
		}
	}

	public void PlaceRoom(){
		GameObject roomBase = PickRoom ();
		roomBase = (GameObject)Instantiate (roomBase);
		roomBase.transform.parent = transform;
		roomBase.transform.localPosition = Vector3.zero;
	}

	public GameObject PickRoom(){
		GameObject roomBase;
		List<GameObject> possibleRooms = new List<GameObject> ();


		for (int i = 0; i < bases.Length; i++) {
			if (onPath) {
				RoomBaseInfo rbInfo = bases [i].GetComponent<RoomBaseInfo> ();
				bool containsAllOpenings = false;
				foreach (string j in openings) {
					if (rbInfo.roomType.Contains (j) == false) {
						containsAllOpenings = false;
						break;
					} else {
						containsAllOpenings = true;
					}
				}

				if (containsAllOpenings) {
					possibleRooms.Add (bases [i]);
				}
			} else {
				possibleRooms.Add (bases [i]);
			}
		}

		int roomBaseIndex = Random.Range (0, possibleRooms.Count - 1);
		roomBase = possibleRooms [roomBaseIndex];

		return roomBase;
	}
}
