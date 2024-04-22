using UnityEngine;

[CreateAssetMenu(menuName = "QuizQuestion")]
public class QuizQuestion : ScriptableObject
{
    public string question;
    public string[] wrongAnswers ;
    public string correctAnswer;
    public string hint;
}