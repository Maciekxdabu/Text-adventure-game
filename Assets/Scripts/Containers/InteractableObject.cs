using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/Interactable Object", fileName = "Object")]
public class InteractableObject : ScriptableObject
{
    public string noun = "name";
    [TextArea]
    public string description = "Description in room";
    public Interaction[] interactions;

    private bool _taken = false;
    public bool taken { get { return _taken; } }

    public void PlaceInRoom()
    {
        _taken = false;
    }

    public void MarkAsTaken()
    {
        _taken = true;
    }
}
