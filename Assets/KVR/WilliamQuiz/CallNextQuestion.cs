using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallNextQuestion : MonoBehaviour
{
    public HPQuizController controller;
    public AudioSource play;

    public void AskQuestion(GameObject FullscaleObj)
    {
        if(FullscaleObj.activeSelf && !play.isPlaying)
        { controller.PresentQuestion(); }
        else if(FullscaleObj.activeSelf && play.isPlaying)
        {
            play.Stop();
            controller.PresentQuestion();
        }
    }
}
