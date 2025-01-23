using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObjectSpot : MonoBehaviour
{
    public InteractableObject objectRef;

    private AudioSource audioSource;

    // ---------- Unity messages

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // ---------- public methods

    public void RunAudioEvent(AudioEvent audioEvent)
    {
        if (audioEvent.passiveOrOneShot)//passive
        {
            audioSource.clip = audioEvent.clip;
            audioSource.Play();
        }
        else//OneShot
        {
            audioSource.PlayOneShot(audioEvent.clip, 1.0f);
        }
    }
}
