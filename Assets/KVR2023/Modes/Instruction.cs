using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using KVR2023;

public class Instruction : TabletPage
{
    public UnityEvent instructionActivated;
    [SerializeField] private TaskManager taskManager;

    public void StartTask() //Called via UnityEvent by a button on the UI_Tablet Instruction page.
    {
        taskManager.StartCurrentTask();
    }

    public void SkipTask() //Called via UnityEvent by a button on the UI_Tablet Instruction page.
    {
        taskManager.SkipCurrentTask();
    }

    public void ReturnToSkipped()
    {
        taskManager.ReturnToSkipped();
    }

    public void ActivateInstruction() //Called via UnityEvent by a button on the UI_Tablet Mode Select Page.
    {
        bool canActivate = taskManager.ActivateTaskManager(Pages.Instruction);
        if (canActivate)
        {
            instructionActivated?.Invoke();
        }
        else
        {
            Debug.LogError("The task manager is aleady active. You can't activate multiple modes at the same time.");
        }
    }
}
