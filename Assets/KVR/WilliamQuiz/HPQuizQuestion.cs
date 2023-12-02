using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/*
 * Created by William HP
 * Created on 07/11/2023
 * Followed guide at the following link: https://unity3d.college/2017/07/12/howto-build-a-quiz-game-in-unity3d-like-trivia-crack-heads-up/
 * Purpose of this script is to rename the answers to a question so that they are easier to maintain
 */

[CreateAssetMenu]
public class HPQuizQuestion : ScriptableObject
{
    [SerializeField] private string question;
    [SerializeField] private List<string> answers = new List<string>();
    [SerializeField] private int correctAnswer;

    public string Question { get { return question; } set { question = value; } }
    public List<string> Answers { get { return answers; } set { answers = value; } }
    public int CorrectAnswer { get { return correctAnswer; } set { correctAnswer = value; } }
    public bool Asked { get; internal set; }

    /*private void OnValidate()
    {
        if (correctAnswer > answers.Count)
        { correctAnswer = 0; }

        RenameScriptableObjectToMatchQuestionAndAnswer();
    }

    private void RenameScriptableObjectToMatchQuestionAndAnswer()
    {
        string desiredName = string.Format("{0} [{1}]", question.Replace("?", ""), answers[correctAnswer]);

        string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
        string shouldEndWith = "/" + desiredName + ".asset";
        if(assetPath.EndsWith(shouldEndWith) == false)
        {
            //Debug.Log("Want to rename to " + desiredName);
            AssetDatabase.RenameAsset(assetPath, desiredName);
            AssetDatabase.SaveAssets();
        }
    }*/
}
