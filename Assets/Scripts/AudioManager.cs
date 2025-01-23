using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] ObjectSpot[] objectSpots;
    Dictionary<string, ObjectSpot> objectSpotsDictionary = new Dictionary<string, ObjectSpot>();

    // ---------- Unity messages

    private void Awake()
    {
        UnpackObjectSpots();
    }

    // ---------- public methods

    public void UnpackObjectSpots()
    {
        objectSpotsDictionary.Clear();

        for (int i = 0; i < objectSpots.Length; i++)
        {
            objectSpotsDictionary.Add(objectSpots[i].objectRef.noun, objectSpots[i]);
        }
    }

    public void RunAudioEvent(AudioEvent audioEvent)
    {
        if (objectSpotsDictionary.ContainsKey(audioEvent.objectAffected.noun))
        {
            objectSpotsDictionary[audioEvent.objectAffected.noun].RunAudioEvent(audioEvent);
        }
    }
}
