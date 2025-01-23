using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioEvent
{
    public AudioClip clip;
    [Tooltip("false - passive, true - OneShot")]
    public bool passiveOrOneShot = true;
    [Tooltip("Object which passive is to be changed or at which position OneShot should be played")]
    public InteractableObject objectAffected;
}
