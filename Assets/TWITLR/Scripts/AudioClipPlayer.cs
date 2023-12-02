using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;


    public void PlayClip(int index)
    {
        if (audioClips != null && index >= 0 && index < audioClips.Length)
        {
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"Invalid index: {index}. No audio clip played.");
        }
    }
}
