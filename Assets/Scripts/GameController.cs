using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RoomNavigation))]
public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;

    [HideInInspector]
    public RoomNavigation roomNavigation;

    List<string> actionLog = new List<string>();

    // ---------- Unity methods

    void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
    }

    private void Start()
    {
        LogRoomText();
        DisplayLoggedText();
    }

    // ---------- public methods

    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());

        displayText.text = logAsText;
    }

    public void LogRoomText()
    {
        string combinedText = roomNavigation.currentRoom.description + "\n";

        LogStringWithReturn(combinedText);
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }
}
