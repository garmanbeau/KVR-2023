using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sandbox : MonoBehaviour
{
    public UnityEvent sandboxActivated;
    [SerializeField] private TaskManager taskManager;
    
    public void NextTask() //Called via UnityEvent by a button on the UI_Tablet Sandbox page.
    {
        taskManager.NextTask();
    }

    public void PreviousTask() //Called via UnityEvent by a button on the UI_Tablet Sandbox page.
    {
        taskManager.PreviousTask();
    }

    public void StartTask() //Called via UnityEvent by a button on the UI_Tablet Sandbox page.
    {
        taskManager.StartCurrentTask();
    }

    public void ActivateSandbox() //Called via UnityEvent by a button on the UI_Tablet Mode Select Page.
    {
        bool canActivate = taskManager.ActivateTaskManager(0);
        if(canActivate)
        {
            sandboxActivated?.Invoke();
        }
        else
        {
            Debug.LogError("The task manager is aleady active. You can't activate multiple modes at the same time.");
        }
    }
}
