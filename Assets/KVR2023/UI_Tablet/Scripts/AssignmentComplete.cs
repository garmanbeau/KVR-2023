using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignmentComplete : MonoBehaviour
{
    [SerializeField] private GameObject buttonScore;
    [SerializeField] private GameObject buttonExit;
    [SerializeField] private TaskManager taskManager;


    public void ViewScore()
    {
        taskManager.TextUpdateProgress();
        buttonScore.SetActive(false);
    }

    public void ExitVR()
    {
        Application.Quit();
    }
}
