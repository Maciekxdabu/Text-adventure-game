using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{
    [Tooltip("A list of items that can be taken into inventory and later used somewhere")]
    public List<InteractableObject> usableItemList;

    //dictionary for "examine", "take" and "operate" InputActions
    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();
    public Dictionary<string, ActionResponse> operateDictionary = new Dictionary<string, ActionResponse>();

    //interaction Dictionary - its keys are: (InputAction.keyWord, interactableObject.noun)
    public Dictionary<(string, string), Interaction> interactionDictionary = new Dictionary<(string, string), Interaction>();

    [HideInInspector] public List<string> nounsInRoom = new List<string>();

    //a dictionary of ActionResponses for "use" command (keys are item names)
    Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();
    private List<string> nounsInInventory = new List<string>();
    private GameController controller;

    // ---------- Unity methods

    private void Awake()
    {
        controller = GetComponent<GameController>();

        //clear take'able items "taken" flag
        usableItemList.ForEach(x => x.PlaceInRoom());
    }

    // ---------- public methods

    //method for checking if an object in Room is not in player inventory (called from Controller when preparing items)
    //TODO - Should actually check if an Object was flagged as taken
    public string GetObjectsNotInInventory(Room currentRoom, int i)
    {
        InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];

        //if (!nounsInInventory.Contains(interactableInRoom.noun))
        if (!interactableInRoom.taken)
        {
            nounsInRoom.Add(interactableInRoom.noun);
            return interactableInRoom.description;
        }

        return null;
    }

    //prepare useDictionary for items in inventory (store SO references) (only adds new references)
    public void AddActionResponsesToUseDictionary()
    {
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            string noun = nounsInInventory[i];

            InteractableObject interactableObjectInInventory = GetInteractableObjectFromUsablelist(noun);
            if (interactableObjectInInventory == null)
                continue;

            //adds items in inventory with "use" to the use Dictionary (unless they are already there)
            for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++)
            {
                Interaction interaction = interactableObjectInInventory.interactions[j];

                if (interaction.actionResponse == null)
                    continue;

                if (!useDictionary.ContainsKey(noun))
                {
                    useDictionary.Add(noun, interaction.actionResponse);
                }    
            }
        }
    }

    //Get SO reference to the item based on noun
    InteractableObject GetInteractableObjectFromUsablelist(string noun)
    {
        for (int i = 0; i < usableItemList.Count; i++)
        {
            if (usableItemList[i].noun == noun)
            {
                return usableItemList[i];
            }
        }
        return null;
    }

    //Get Interaction reference to the item based on noun and InputAction name
    Interaction GetInteractionFromUsableList(string noun, string inputAction)
    {
        InteractableObject interactableObject = GetInteractableObjectFromUsablelist(noun);

        for (int i = 0; i < interactableObject.interactions.Length; i++)
        {
            if (interactableObject.interactions[i].inputAction.keyWord == inputAction)
                return interactableObject.interactions[i];
        }

        return null;
    }

    public void DisplayInventory()
    {
        controller.LogStringWithReturn("You look in your backpack, inside you have: ");

        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            controller.LogStringWithReturn(nounsInInventory[i]);
        }
    }

    public void ClearCollections()
    {
        examineDictionary.Clear();
        takeDictionary.Clear();
        operateDictionary.Clear();
        interactionDictionary.Clear();
        nounsInRoom.Clear();
    }

    //returns a takeDictionary for "take" InputAction
    //also removes the item from Room if it was taken successfully
    public Dictionary<string, string> Take (string[] separatedInputWords)
    {
        string noun = separatedInputWords[1];

        if (nounsInRoom.Contains(noun))
        {
            nounsInInventory.Add(noun);
            //rebuilt the useDictionary after adding an item
            AddActionResponsesToUseDictionary();
            GetInteractableObjectFromUsablelist(noun).MarkAsTaken();
            nounsInRoom.Remove(noun);
            return takeDictionary;
        }
        else
        {
            controller.LogStringWithReturn("There is no " + noun + " here to take.");
            return null;
        }
    }

    //methods for "use" InputAction to use item from inventory
    public void UseItem(string[] separatedInputWords)
    {
        string nounToUse = separatedInputWords[1];

        if (nounsInInventory.Contains(nounToUse))
        {
            if (useDictionary.ContainsKey(nounToUse))
            {
                bool actionResult = useDictionary[nounToUse].DoActionResponse(controller);
                if (!actionResult)
                {
                    controller.LogStringWithReturn("Hmm. Nothing happens.");
                }
                else
                {
                    controller.LogStringWithReturn(useDictionary[nounToUse].successResponse);

                    //AudioEvent (after "use" success)
                    controller.interactableItems.TryRunAudioEvent(separatedInputWords);
                }
            }
            else
            {
                controller.LogStringWithReturn("You can't use the " + nounToUse);
            }
        }
        else
        {
            controller.LogStringWithReturn("There is no " + nounToUse + " in your inventory");
        }
    }

    //method for "operate" InputAction to operate items in rooms
    public void OperateItem(string[] separatedInputWords)
    {
        string nounToUse = separatedInputWords[1];

        if (operateDictionary.ContainsKey(nounToUse))
        {
            bool actionResult = operateDictionary[nounToUse].DoActionResponse(controller);
            if (!actionResult)
            {
                controller.LogStringWithReturn("Hmm. Nothing happens.");
            }
            else
            {
                controller.LogStringWithReturn(operateDictionary[nounToUse].successResponse);
            }
        }
        else
        {
            controller.LogStringWithReturn("You can't operate the " + nounToUse);
        }
    }

    public void UnpackItemsToDictionary()
    {
        for (int i = 0; i < usableItemList.Count; i++)
        {
            InteractableObject interactableObject = usableItemList[i];
            for (int j = 0; j < interactableObject.interactions.Length; j++)
            {
                Interaction interaction = interactableObject.interactions[j];

                if (!interactionDictionary.ContainsKey((interaction.inputAction.keyWord, interactableObject.noun)))
                    interactionDictionary.Add((interaction.inputAction.keyWord, interactableObject.noun), interaction);
            }
        }
    }

    //Runs AudioEvent's on the given Object Interaction's (if an Object with such an Interaction exists in the dictionary)
    public void TryRunAudioEvent(string[] separatedInputWords)
    {
        if (interactionDictionary.ContainsKey((separatedInputWords[0], separatedInputWords[1])))
        {
            AudioEvent[] audioEvents = interactionDictionary[(separatedInputWords[0], separatedInputWords[1])].audioEvents;
            for (int i = 0; i < audioEvents.Length; i++)
            {
                controller.audioManager.RunAudioEvent(audioEvents[i]);
            }
        }
    }
}
