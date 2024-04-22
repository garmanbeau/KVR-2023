using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAffordance : MonoBehaviour, IAffordance
{
    public AudioSource affordanceAudio;

    public int timeAffordancePlayed;

    private void Start()
    {
        if (affordanceAudio == null) {

            Debug.Log("Affordance Audio must have a reference to an AudioSource");
            return;
        }
        affordanceAudio.Stop();
    }
    public void Trigger()
    {
        StartCoroutine(PlayAudioAffordance());
    }
    public void StopAffordance()
    {
        StopAllCoroutines();
    }

    IEnumerator PlayAudioAffordance() { 
        
        if (affordanceAudio != null) { 
        
            affordanceAudio.Play();

            yield return new WaitForSeconds(timeAffordancePlayed);

            affordanceAudio.Stop();
        
        }
    }

}
