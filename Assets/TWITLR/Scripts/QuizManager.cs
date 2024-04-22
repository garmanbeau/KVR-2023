// 2023-11-28 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class QuizManager : MonoBehaviour
{
    public TMP_Text questionText;
    public Button[] answerButtons;
    public TMP_Text hint;
    public List<QuizQuestion> questions;
    public UnityEvent correctAnswerEvent;
    public UnityEvent incorrectAnswerEvent;
    public UnityEvent allQuestionsAnsweredEvent;
    public int questionsToComplete;
    public bool allowIncorrect;

    private QuizQuestion currentQuestion;
    private int correctAnswerIndex;
    private int correctAnswers;
    public float nextQuestionDelay = 2;

    void Start()
    {
        if (questions.Count < questionsToComplete)
        {
            Debug.LogWarning("Not enough questions to fulfil the required amount of questions to complete the quiz");
            return;
        }

        correctAnswers = 0;
        currentQuestion = SelectRandomQuestion();
        questionText.text = currentQuestion.question;
        hint.text = currentQuestion.hint;
        RandomlyAssignAnswers();
    }

    // 2023-12-19 AI-Tag 
    // This was created with assistance from Muse, a Unity Artificial Intelligence product

    QuizQuestion SelectRandomQuestion()
    {
        if (questions.Count == 0)
        {
            Debug.Log("All questions have been asked.");
            return null;
        }

        int index = Random.Range(0, questions.Count);
        QuizQuestion selectedQuestion = questions[index];
        questions.RemoveAt(index); // Remove the selected question from the list
        return selectedQuestion;
    }


    void RandomlyAssignAnswers()
    {
        List<string> answers = new List<string>(currentQuestion.wrongAnswers);
        correctAnswerIndex = Random.Range(0, answerButtons.Length);

        answers.Insert(correctAnswerIndex, currentQuestion.correctAnswer);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = answers[i];
            int buttonIndex = i;
            answerButtons[i].onClick.AddListener(() => CheckAnswer(buttonIndex, allowIncorrect));
        }
    }

    public void NextQuestion()
    {
        currentQuestion = SelectRandomQuestion();
        questionText.text = currentQuestion.question;
        hint.text = currentQuestion.hint;
        RandomlyAssignAnswers();
    }

    void DisableButtons()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {

            answerButtons[i].onClick.RemoveAllListeners();
        }
    }

    void EnableButtons()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
           
            int buttonIndex = i;
            answerButtons[i].onClick.AddListener(() => CheckAnswer(buttonIndex, allowIncorrect));
        }

    }
    public void CheckAnswer(int buttonIndex, bool allowIncorrect = false)
    {
      
        DisableButtons();
        if (buttonIndex == correctAnswerIndex)
        {
            Debug.Log("Correct answer!");
            correctAnswers++;
            correctAnswerEvent.Invoke();


            if (correctAnswers == questionsToComplete)
            {
                Debug.Log("All questions correctly answered!");

                allQuestionsAnsweredEvent.Invoke();
            }
            else
            {

                Invoke("NextQuestion", nextQuestionDelay);
            }


        }
        else if (buttonIndex != correctAnswerIndex && allowIncorrect) 
        {
            correctAnswers++;
            incorrectAnswerEvent.Invoke();

            if (correctAnswers == questionsToComplete)
            {
                Debug.Log("All questions correctly answered!");

                allQuestionsAnsweredEvent.Invoke();
            }
            else
            {

                Invoke("NextQuestion", nextQuestionDelay);
            }
        }
        else 
        {
            EnableButtons();

            Debug.Log("Wrong answer! Try again.");
            incorrectAnswerEvent.Invoke();
            // Logic for wrong answer goes here
        }
    }
}
