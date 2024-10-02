using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_LevelGenerator : MonoBehaviour {

	public int levelWidth, levelHeight, roomSize;
	public Material gridMat1, gridMat2;
	GameObject levelGrid, levelRooms;

	public F_Room[,] rooms;

	public GameObject roomPrefab, player;
	public F_Room currentRoom;

	// Use this for initialization
	void Start () {
		GenerateGrid ();
		CreateRooms ();
		CreatePath ();
	}

	void CreateRooms(){
		levelRooms = new GameObject ("_LevelRooms");
		levelRooms.transform.position = new Vector3 ((-levelWidth / 2)*roomSize, 0, (levelHeight  / 2)*roomSize);

		rooms = new F_Room[levelWidth, levelHeight];

		for (int y = 0; y < levelWidth; y++) {
			for (int x = 0; x < levelHeight; x++) {
				GameObject room = (GameObject)Instantiate (roomPrefab, levelRooms.transform);
				room.name = "Room : " + x + "," + y;
				room.transform.localPosition = new Vector3 (x * roomSize, 0, -y * roomSize);
				F_Room roomInfo = room.GetComponent<F_Room> ();
				roomInfo.gridX = x;
				roomInfo.gridY = y;
				roomInfo.UpdateInfo ();

				rooms [x, y] = roomInfo;
			}
		}
	}

	void CreatePath(){
		int entranceX = Random.Range (0, levelWidth - 1);
		currentRoom = rooms[entranceX, 0];

		F_Room roomInfo = rooms [entranceX, 0].GetComponent<F_Room> ();
		roomInfo.onPath = true;
		roomInfo.isEntrance = true;
		roomInfo.UpdateInfo ();

		player.transform.position = roomInfo.transform.position;

		FindNextRoom ();
	}

	void FindNextRoom(){
		currentRoom.neighbours = GetNeighbours (currentRoom);

		if (currentRoom.neighbours.Count > 0) {
			int nextIndex = Random.Range (0, currentRoom.neighbours.Count);

			F_Room nextRoom = currentRoom.neighbours [nextIndex];
			nextRoom.onPath = true;

			//Calculate Room Type (corrider, drop in, drop from, etc.) =========
			if (nextRoom.gridX == currentRoom.gridX - 1) {
				//Right -> Left
				currentRoom.openings.Add ("L");
				nextRoom.openings.Add ("R");
			}

			if (nextRoom.gridX == currentRoom.gridX + 1) {
				//Left -> Right
				currentRoom.openings.Add ("R");
				nextRoom.openings.Add ("L");
			}

			if (nextRoom.gridY == currentRoom.gridY + 1) {
				//Up -> Down
				currentRoom.openings.Add ("D");
				nextRoom.openings.Add ("U");
			}

			nextRoom.UpdateInfo ();


			currentRoom = nextRoom;
			FindNextRoom ();
		} else {
			currentRoom.isExit = true;
			currentRoom.UpdateInfo ();
			PlaceRooms ();
		}
		
	}

	public void PlaceRooms(){
		foreach (F_Room room in rooms) {
			room.PlaceRoom ();
		}
	}

	public List<F_Room> GetNeighbours(F_Room room){
		List<F_Room> neighbours = new List<F_Room> ();
		//Left Room
		if (room.gridX - 1 >= 0) {
			F_Room leftRoom = rooms [room.gridX - 1, room.gridY];
			if (!leftRoom.onPath) {
				neighbours.Add (leftRoom);
			}
		}
		//Right Room
		if (room.gridX + 1 < levelWidth) {
			F_Room rightRoom = rooms [room.gridX + 1, room.gridY];
			if (!rightRoom.onPath) {
				neighbours.Add (rightRoom);
			}
		}
		//Down Room
		if (room.gridY < levelHeight - 1) {
			F_Room downRoom = rooms [room.gridX, room.gridY + 1];
			if (!downRoom.onPath) {
				neighbours.Add (downRoom);
			}
		}

		return neighbours;
	}

	void GenerateGrid(){
		levelGrid = new GameObject("_LevelGrid");
		levelGrid.transform.position = new Vector3 ((-levelWidth / 2)*roomSize, 0, (levelHeight  / 2)*roomSize);
		Material activeGridMat = gridMat1;
		for (int y = 0; y < levelWidth; y++) {
			for (int x = 0; x < levelHeight; x++) {
				GameObject floorTile = GameObject.CreatePrimitive(PrimitiveType.Quad);
				floorTile.transform.parent = levelGrid.transform;
				floorTile.transform.localScale = new Vector3 (roomSize, roomSize, 1);
				floorTile.transform.localEulerAngles = new Vector3 (90, 0, 0);
				floorTile.transform.localPosition = new Vector3 (x * roomSize, 0, -y * roomSize);
				floorTile.GetComponent<MeshRenderer> ().material = activeGridMat;
				//Debug.Log ("Generated floor tile at position : " + floorTile.transform.position);

				if (activeGridMat == gridMat1) {
					activeGridMat = gridMat2;
				} else {
					activeGridMat = gridMat1;
				}
			}
		}
	}
}
