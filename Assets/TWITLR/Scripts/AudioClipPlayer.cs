using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    bool[] clipPlayed;

    public AudioClip startupClip;

    private void Start()
    {
        clipPlayed = new bool[audioClips.Length];
       for (int i = 0; i < clipPlayed.Length; i++) { clipPlayed[i] = false; }

       if (startupClip != null)
        {
            audioSource.clip = startupClip;
            audioSource.Play();
        }
       
    }

    public void PlayOnce(int clip)
    {
        if (clipPlayed[clip]) { return; }

        clipPlayed[clip] = true;

       
            audioSource.clip = audioClips[clip];
            audioSource.Play();
      
    }

    public void PlayClip(int index)
    {
        if (audioClips != null && index >= 0 && index < audioClips.Length && !audioSource.isPlaying)
        {
            audioSource.clip = audioClips[index];
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"Invalid index: {index}. No audio clip played.");
        }
    }
}
