using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * Created by William HP
 * Created on 07/11/2023
 * Followed guide at the following link: https://unity3d.college/2017/07/12/howto-build-a-quiz-game-in-unity3d-like-trivia-crack-heads-up/
 * This script looks for a folder called Questions in the Resource folder within the Assset folder of the project and loads all questions and answers it finds
 */


public class HPQuestionCollection : MonoBehaviour
{
    public HPQuizQuestion[] allQuestions;

    private void Awake()
    {
        LoadAllQuestions();
    }

    public void PublicLoadAllQuestions()
    { LoadAllQuestions();  }
    private void LoadAllQuestions()//This is where we go looking for the folder and any questions within
    {
        allQuestions = Resources.LoadAll<HPQuizQuestion>("HPQuestions");
    }

    public HPQuizQuestion FirstQuestion()
    {
        var question = allQuestions[0];
        return question;
    }
    public HPQuizQuestion GetUnaskedQuestion(HPQuizQuestion q)
    {
        int i = 0;
        for(int j = 0; j < allQuestions.Length; j++)
        {
            if(q == allQuestions[j])
            { i = j + 1; }
        }
        //ResetQuestionsIfAllhaveBeenAsked();//This makes sure all questions can be asked again. To stop a never-ending quiz, just comment this line out

        //var question = allQuestions.Where(t => t.Asked == false).OrderBy(t => Random.Range(0, int.MaxValue)).FirstOrDefault();//If you want to randomize your questions, uncomment this line of code and comment out lines 35-40 and 44-46
        if (i+1 > allQuestions.Length)
        { i = 0; }
        var question = allQuestions[i];

        question.Asked = true;
        return question;
    }

    private void ResetQuestionsIfAllhaveBeenAsked()
    {
        if(allQuestions.Any(t => t.Asked == false) == false)
        { ResetQuestions(); }
    }

    private void ResetQuestions()
    {
        foreach(var q in allQuestions)
        { q.Asked = false; }
    }

    public int WhereInList(HPQuizQuestion q)
    {
        int location = 0;
        for(int i = 0; i < allQuestions.Length; i++)
        {
            if(q == allQuestions[i])
            { location = i; }
        }
        return location;
    }
}
