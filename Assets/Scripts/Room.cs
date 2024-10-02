using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room : MonoBehaviour
{
    // room requirements
    public bool onPath, isEntrance, isExit;

    // room position
    public int gridX, gridY;

    TMP_Text roomLabel;
    public List<Room> neighbours;
    public List<string> openings = new List<string>();

    void Awake()
    {
        roomLabel = GetComponentInChildren<TMP_Text>();
        
    }

    public void UpdateInfo()
    {
        roomLabel.text = ("[" + gridX + "," + gridY + "]");

        if (onPath)
        {
            roomLabel.color = Color.yellow;
        }
        if (isEntrance)
        {
            roomLabel.color = Color.green;
        }
        if (isExit)
        {
            roomLabel.color = Color.red;
        }
    }
}
