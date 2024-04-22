using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using KVR2023;
using UnityEngine.UI;

public class Sandbox : TabletPage
{
    public UnityEvent sandboxActivated;
    [SerializeField] private TaskManager taskManager;
    private HorizontalLayoutGroup layoutGroup;
    public UnityEvent submitAssignment;
    public List<GameObject> buttons;
    [SerializeField] private GameObject beginButton;
    [SerializeField] private GameObject continueButton;
    private static readonly float desiredSpacing = .04f;
    private static readonly int hierarchyIndexButtonGroup = 1;

    private void Awake()
    {
        layoutGroup = this.gameObject.transform.GetChild(hierarchyIndexButtonGroup).gameObject.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.spacing = 0f;
    }

    public void BeginPressed()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(true);
        }
        beginButton.SetActive(false);
        layoutGroup.spacing = desiredSpacing;
        taskManager.CurrentTask.IsComplete = true;
        taskManager.CurrentTask.IsActive = false;
        taskManager.NextTask();
    }

    public void ContinuePressed()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(true);
        }
        continueButton.SetActive(false);
        layoutGroup.spacing = desiredSpacing;
        taskManager.SafeTextUpdate();
    }

    public void SubmitAssignment()
    {
        bool canSubmit = taskManager.CanSubmitAssignment();
        if (canSubmit)
        {
            submitAssignment?.Invoke();
            tabletContext.LoadSpecificPage((int)Pages.Complete);
        }
    }

    public void ResetPage()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
        continueButton.SetActive(true);
        layoutGroup.spacing = 0f;
        taskManager.SetContinueText();
    }

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
        bool canActivate = taskManager.ActivateTaskManager(Pages.Sandbox);
        if (canActivate)
        {
            sandboxActivated?.Invoke();
        }
        else
        {
            Debug.LogError("The task manager is aleady active. You can't activate multiple modes at the same time.");
        }
    }
}
