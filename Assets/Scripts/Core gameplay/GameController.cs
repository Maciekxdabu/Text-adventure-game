using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RoomNavigation), typeof(TextInput), typeof(FlagManager))]
public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;
    public InputAction[] inputActions;

    [HideInInspector]
    public RoomNavigation roomNavigation;
    [HideInInspector]
    public List<string> interactionDescriptionsInRoom = new List<string>();
    [HideInInspector]
    public InteractableItems interactableItems;
    [HideInInspector]
    public FlagManager flagManager;

    List<string> actionLog = new List<string>();

    // ---------- Unity methods

    void Awake()
    {
        interactableItems = GetComponent<InteractableItems>();
        roomNavigation = GetComponent<RoomNavigation>();
        flagManager = GetComponent<FlagManager>();
    }

    private void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    // ---------- public methods

    //Dislays a text in the console (refreshes the display)
    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());

        displayText.text = logAsText;
    }

    /// <summary>
    /// Clears and unpacks a Room and then logs its description (does not display by itself)
    /// </summary>
    public void DisplayRoomText()
    {
        ClearCollectionsForNewRoom();
        UnpackRoom();

        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());

        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;

        LogStringWithReturn(combinedText);
    }

    //Adds a new entry to logs and a new line symbol just after
    public void LogStringWithReturn(string stringToAdd)
    {
        if (stringToAdd == null || stringToAdd == "")
            return;

        actionLog.Add(stringToAdd + "\n");
    }

    //check if object can be examined, taken, etc. (item==noun, action==verb)
    public string TestVerbDictionaryWithNoun(Dictionary<string, string> verbDictionary, string verb, string noun)
    {
        if (verbDictionary.ContainsKey(noun))
        {
            return verbDictionary[noun];
        }

        return "You can't " + verb + " " + noun;
    }

    // ---------- private methods

    //unpacks the Room (prepares exits and interactions)
    private void UnpackRoom()
    {
        roomNavigation.UnpackExitsInRoom();
        PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);
    }

    //Prepares the possible interactions in the current Room
    private void PrepareObjectsToTakeOrExamine(Room currentRoom)
    {
        for (int i = 0; i < currentRoom.interactableObjectsInRoom.Length; i++)
        {
            InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];
            if (!interactableInRoom.taken && FlagManager.AreFlagsMet(interactableInRoom.flags))
            {
                //prepare the interaction descriptions in a room
                string descriptionNotInInventory = interactableItems.GetObjectsNotInInventory(currentRoom, i);
                if (descriptionNotInInventory != null && interactableInRoom.invisibleInRoom == false)
                {
                    interactionDescriptionsInRoom.Add(descriptionNotInInventory);
                }

                //prepare interaction dictionaries (examine and take)
                for (int j = 0; j < interactableInRoom.interactions.Length; j++)
                {

                    Interaction interaction = interactableInRoom.interactions[j];
                    switch (interaction.inputAction.keyWord)
                    {
                        case "examine":
                            interactableItems.examineDictionary.Add(interactableInRoom.noun, interaction.GetResponseBasedOnFlags());
                            break;
                        case "take":
                            interactableItems.takeDictionary.Add(interactableInRoom.noun, interaction.GetResponseBasedOnFlags());
                            break;
                        default: break;
                    }

                }
            }
        }
    }

    private void ClearCollectionsForNewRoom()
    {
        interactableItems.ClearCollections();
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
    }
}
