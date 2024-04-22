using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletePage : TabletPage
{
    [SerializeField] private GameObject buttonScore;
    [SerializeField] private GameObject buttonExit;
    [SerializeField] private TaskManager taskManager;
    private static readonly int hierarchyIndexScoreText = 2;

    public void ViewScore()
    {
        string progressString = taskManager.GetProgressString();
        this.UpdateText(hierarchyIndexScoreText, progressString); //To Do: Get rid of magic number.
        buttonScore.SetActive(false);
    }

    public void ExitVR()
    {
        Application.Quit();
    }
}
