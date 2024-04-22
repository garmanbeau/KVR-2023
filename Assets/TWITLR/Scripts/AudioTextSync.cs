using UnityEngine;
using TMPro;
using System.Collections;

public class AudioTextSync : MonoBehaviour
{
    public AudioClip[] audioClips;
    public string[] textForClips;
    public AudioSource audioSource;
    public TMP_Text textDisplay;

    int index = 0;

   public void ClearText()
    {
        textDisplay.text = "";
    }

    public void PlayClipWithText()
    {
        if (audioSource.isPlaying) { return; }


        if (audioClips != null && textForClips != null && index >= 0 && index < audioClips.Length && index < textForClips.Length)
        {
            audioSource.clip = audioClips[index];
            audioSource.Play();
            StartCoroutine(DisplayTextCoroutine(textForClips[index], audioSource.clip.length));
        }
        else
        {
            Debug.LogWarning($"Invalid index: {index}. No audio clip and text displayed.");
        }

        index++;
        if (index >= textForClips.Length)
        {
            index = 0;
        }
    }

    private IEnumerator DisplayTextCoroutine(string text, float duration)
    {
        textDisplay.text = "";
        float timePerCharacter = duration / text.Length;
        for (int i = 0; i < text.Length; i++)
        {
            textDisplay.text += text[i];
            yield return new WaitForSeconds(timePerCharacter);
        }
        Invoke("ClearText", 4);
    }
}
