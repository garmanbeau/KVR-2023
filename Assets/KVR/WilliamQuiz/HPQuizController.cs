using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by William HP
 * Created on 07/11/2023
 * Followed guide at the following link: https://unity3d.college/2017/07/12/howto-build-a-quiz-game-in-unity3d-like-trivia-crack-heads-up/
 */

public class HPQuizController : MonoBehaviour
{
    private HPQuestionCollection questionCollection;
    private HPQuizQuestion currentQuestion;
    private HPuiController UIcontroller;

    [SerializeField] private float delayBetweenQuestions = 3f;
    public GameObject[] Parts;
    public GameObject[] Particles;
    public AudioClip[] CorrectClips;
    public AudioClip[] WrongClips;
    public AudioClip[] Question;
    public AudioSource play;
    int counter = 0;
  //  public HPShowCurrentQuestionData Show;

    private void Awake()
    {
        questionCollection = FindObjectOfType<HPQuestionCollection>();
        UIcontroller = FindObjectOfType<HPuiController>();
    }

    void Start()
    {
        //PresentQuestion();   
    }

    public void FirstQuestion()
    {
        currentQuestion = questionCollection.FirstQuestion();
        UIcontroller.SetupUIForQuestion(currentQuestion);
        AskQuestion();
      //  Show.ShowData(currentQuestion);
    }

    public void PresentQuestion()
    {
        currentQuestion = questionCollection.GetUnaskedQuestion(currentQuestion);
        UIcontroller.SetupUIForQuestion(currentQuestion);
        AskQuestion();
      //  Show.ShowData(currentQuestion);
    }

    public void SubmitAnswer(int answerNumber)
    {
        bool isCorrect = answerNumber == currentQuestion.CorrectAnswer;
        UIcontroller.HandleSubmittedAnswer(isCorrect);

        //StartCoroutine(ShowNextQuestionAfterDelay());
    }

    public void TryAgain()
    { StartCoroutine(RetryQuestion()); }

    public void ShowPath()
    {
        foreach(GameObject obj in Particles)
        {
            obj.SetActive(true);
        }
        var temp = Parts[counter];
        PcPartLocation part = temp.GetComponent<PcPartLocation>();


        if (part != null)
        {
            part.GrabThis = true;
           
            if (part.otherPcParts.Count > 0)
            {
                foreach (var p in part.otherPcParts)
                {
                    p.GrabThis = true;
                }
               // temp = Parts[counter];
               // temp.GetComponent<PcPartLocation>().GrabThis = true;
               // counter += 1;
            }
            counter += 1;
        }
      /* else if(temp.GetComponent<CablePartLocation>() != null)
        { 
            temp.GetComponent<CablePartLocation>().GrabThis = true;
        }*/
    }

    public void PlayCorrectAnswerAudio()
    {
        int clip = questionCollection.WhereInList(currentQuestion);
        if (play.isPlaying)
        {
            play.Stop();
        }
        play.PlayOneShot(CorrectClips[clip]);
    }

    public void PlayWrongAnswerAudio()
    {
        int clip = questionCollection.WhereInList(currentQuestion);
        if (play.isPlaying)
        {
            play.Stop();
        }
        play.PlayOneShot(WrongClips[clip]);
    }

    public void AskQuestion()
    {
        int clip = questionCollection.WhereInList(currentQuestion);
        if (play.isPlaying)
        {
            play.Stop();
        }
        play.PlayOneShot(Question[clip]);
    }

    public void EnterDebug()
    {
        if(play.isPlaying)
        { play.Stop(); }
        foreach(GameObject obj in Parts)
        {
            var part = obj.GetComponent<PcPartLocation>();
            part.GrabThis = true;
            if(part.otherPcParts.Count > 0)
            {
                foreach(PcPartLocation other in part.otherPcParts)
                {
                    other.GrabThis = true;
                }
            }
        }

        var ui = GameObject.Find("UI");
        ui.SetActive(false);
    }
    
    private IEnumerator RetryQuestion()
    {
        yield return new WaitForSeconds(delayBetweenQuestions);
        //PresentQuestion();
        UIcontroller.SetupUIForQuestion(currentQuestion);
    }

}
