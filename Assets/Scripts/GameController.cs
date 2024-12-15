using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RoomNavigation), typeof(TextInput))]
public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;
    public InputAction[] inputActions;

    [HideInInspector]
    public RoomNavigation roomNavigation;
    [HideInInspector]
    public List<string> interactionDescriptionsInRoom = new List<string>();

    List<string> actionLog = new List<string>();

    // ---------- Unity methods

    void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
    }

    private void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    // ---------- public methods

    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());

        displayText.text = logAsText;
    }

    public void DisplayRoomText()
    {
        ClearCollectionsForNewRoom();
        UnpackRoom();

        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());

        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;

        LogStringWithReturn(combinedText);
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    // ---------- private methods

    private void UnpackRoom()
    {
        roomNavigation.UnpackExitsInRoom();
    }

    private void ClearCollectionsForNewRoom()
    {
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
    }
}
