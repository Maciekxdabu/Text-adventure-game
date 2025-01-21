using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/Interactable Object", fileName = "Object")]
public class InteractableObject : ScriptableObject
{
    public string noun = "name";
    [Tooltip("Object is interactable, but does not show in the Room description (e.g. put info in Actions like \"examine\" or \"search\")")]
    public bool invisibleInRoom = false;
    [Tooltip("Object does not appear and is not interactable until all flags are met")]
    public Flag[] flags;
    [TextArea, Tooltip("Description of the item in room (make sure to use <b></b> to mark the object keyword)")]
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
