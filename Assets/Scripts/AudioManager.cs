using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] ObjectSpot[] objectSpots;
    Dictionary<string, ObjectSpot> objectSpotsDictionary = new Dictionary<string, ObjectSpot>();

    [SerializeField] private AudioSource footStepsAudioSource;

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

    public ObjectSpot GetObjectSpot(string objectNoun)
    {
        if (objectSpotsDictionary.ContainsKey(objectNoun))
            return objectSpotsDictionary[objectNoun];
        else
            return null;
    }

    public void ActivateFootsteps(bool value)
    {
        footStepsAudioSource.mute = !value;
    }

    public void ChangeFootstepsMixer(AudioMixerGroup mixerGroup)
    {
        footStepsAudioSource.outputAudioMixerGroup = mixerGroup;
    }
}
