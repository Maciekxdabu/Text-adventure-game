using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Text Adventure/Room", fileName = "Room")]
public class Room : ScriptableObject
{
    [TextArea]
    public string description;
    public string roomName;
    public AudioMixerGroup audioMixer;
    public Exit[] exits;
    public InteractableObject[] interactableObjectsInRoom;
}
