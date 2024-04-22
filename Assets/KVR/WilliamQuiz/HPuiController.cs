using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Created by William HP
 * Created on 07/11/2023
 * Followed guide at the following link: https://unity3d.college/2017/07/12/howto-build-a-quiz-game-in-unity3d-like-trivia-crack-heads-up/
 * This scripts main job is to control the UI and update it based on if the correct answer is selected
 */

public class HPuiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] answerButtons;
    [SerializeField] private GameObject correctPopup;
    [SerializeField] private GameObject incorrectPopup;
    [SerializeField] private HPQuizController controller;
    [SerializeField] private HPQuestionCollection collector;
    public DialogueWriter writer;
    public AudioSource play;

    public void SetupUIForQuestion(HPQuizQuestion question)
    {
        correctPopup.SetActive(false);
        incorrectPopup.SetActive(false);

        questionText.text = question.Question;

        for(int i = 0; i < question.Answers.Count; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.Answers[i];
            answerButtons[i].gameObject.SetActive(true);
        }

        for(int i = question.Answers.Count; i < answerButtons.Length; i++)
        { answerButtons[i].gameObject.SetActive(false); }
        writer.EmptyWindow();
        for(int i = 0; i < collector.allQuestions.Length; i++)
        {
            if(question == collector.allQuestions[i])
            { writer.counter = i; }
        }

    }

    public void HandleSubmittedAnswer(bool isCorrect)
    {
        ToggleAnswerButtons(false);

        if(isCorrect)
        { 
            correctPopup.SetActive(true); 
            controller.ShowPath(); 
            writer.StartCorrectDialouge();
            controller.PlayCorrectAnswerAudio();
        }
        else
        {
            incorrectPopup.SetActive(true);
            controller.TryAgain();
            writer.StartWrongDialogue();
            controller.PlayWrongAnswerAudio();
        }
    }

    private void Update()
    {
        if(play.isPlaying)
        {
            foreach(Button b in answerButtons)
            { b.interactable = false; }
        }
        else if(!play.isPlaying)
        {
            foreach (Button b in answerButtons)
            { b.interactable = true; }
        }
    }

    private void ToggleAnswerButtons(bool value)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        { answerButtons[i].gameObject.SetActive(value); }
    }
}
