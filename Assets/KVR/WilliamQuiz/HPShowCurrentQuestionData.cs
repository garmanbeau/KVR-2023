using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPShowCurrentQuestionData : MonoBehaviour
{
    public TextMeshProUGUI QuestionText;
    public TextMeshProUGUI[] AnswerText;
    public TextMeshProUGUI CorrectAnswer;
    private HPQuizQuestion currentQuestion;


    public void ShowData(HPQuizQuestion q)
    {
        currentQuestion = q;
        QuestionText.text = q.Question;
        for(int i = 0; i < q.Answers.Count; i++)
        { 
            AnswerText[i].text = q.Answers[i]; 
            AnswerText[i].gameObject.SetActive(true); 
        }

        for (int i = q.Answers.Count; i < AnswerText.Length; i++)
        { AnswerText[i].gameObject.SetActive(false); }

        CorrectAnswer.text = q.CorrectAnswer.ToString();
    }

    public void UpdateAnswer(TextMeshProUGUI text)
    {
        string temp = text.text;
        int Answer = 0;
        for(int i = 0; i < AnswerText.Length; i++)
        {
            if(text == AnswerText[i])
            { Answer = i; break; }
        }

        currentQuestion.Answers[Answer] = temp;
    }

    public void SetCorrectAnswer(TMP_InputField text)
    {
        string temp = text.text;
        int number;
        Debug.Log("String to Convert: " + temp);
        number = int.Parse(temp);
        Debug.Log("Attempted to change correct answer to " + number);
        currentQuestion.CorrectAnswer = number;

    }
}
