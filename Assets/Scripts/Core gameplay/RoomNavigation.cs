using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoomNavigation : MonoBehaviour
{
    [SerializeField] Transform listener;
    public Room currentRoom;

    [SerializeField] RoomSpot[] roomSpots;
    Dictionary<string, Transform> roomSpotsDictionary = new Dictionary<string, Transform>();

    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();

    private GameController controller;

    // ---------- Unity methods

    private void Awake()
    {
        controller = GetComponent<GameController>();

        UnpackRoomSpots();
    }

    // ---------- public methods

    //unpacks the exits in the current Room
    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < currentRoom.exits.Length; i++)
        {
            if (FlagManager.AreFlagsMet(currentRoom.exits[i].flags))//check if exit flags are met before adding an exit to interactions
            {
                exitDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);
                controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
            }
        }
    }

    public void ClearExits()
    {
        exitDictionary.Clear();
    }

    public void AttemptToChangeRooms(string directionNoun)
    {
        if (exitDictionary.ContainsKey(directionNoun))
        {
            currentRoom = exitDictionary[directionNoun];

            if (roomSpotsDictionary.ContainsKey(currentRoom.name))
            {
                listener.DOMove(roomSpotsDictionary[currentRoom.name].position, 3f);
            }

            controller.LogStringWithReturn("You head off to the " + directionNoun);
            //controller.DisplayRoomText();
        }
        else
        {
            controller.LogStringWithReturn("There is no path to the " + directionNoun);
        }
    }

    //Unpacks a Transform dictionary of RoomSpot's for later navigation
    public void UnpackRoomSpots()
    {
        roomSpotsDictionary.Clear();

        for (int i=0; i<roomSpots.Length; i++)
        {
            roomSpotsDictionary.Add(roomSpots[i].room.name, roomSpots[i].transform);
        }
    }
}
