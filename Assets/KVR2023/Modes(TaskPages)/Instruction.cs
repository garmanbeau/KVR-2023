using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using KVR2023;
using UnityEngine.UI;

public class Instruction : TabletPage
{
    public UnityEvent instructionActivated;
    [SerializeField] private TaskManager taskManager;
    private HorizontalLayoutGroup layoutGroup;
    public UnityEvent submitAssignment;
    public List<GameObject> buttons;
    [SerializeField] private GameObject beginButton;
    [SerializeField] private GameObject continueButton;
    private static readonly float desiredSpacing = .04f;
    private static readonly int hierarchyIndexButtonGroup = 1;

    private void Awake() //Invoked by the engine when an instance of this class is instantiated (or activated if instantiated as inactive).
    {
        layoutGroup = this.gameObject.transform.GetChild(hierarchyIndexButtonGroup).gameObject.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.spacing = 0f;
    }

    public void BeginPressed() //Called via OnClick? event of a TabletButton.
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

    public void ContinuePressed() //Called via OnClick? event of a TabletButton.
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(true);
        }
        continueButton.SetActive(false);
        layoutGroup.spacing = desiredSpacing;
        taskManager.SafeTextUpdate();
    }

    public void SubmitAssignment() //Called via OnClick? event of a TabletButton.
    {
        bool canSubmit = taskManager.CanSubmitAssignment();
        if (canSubmit)
        {
            submitAssignment?.Invoke();
            tabletContext.LoadSpecificPage((int)Pages.Complete);
        }
    }

    public void ResetPage() //Invoked by TaskManager to reset the page before switching to the sub-task page.
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
        continueButton.SetActive(true);
        layoutGroup.spacing = 0f;
        taskManager.SetContinueText();
    }

    public void StartTask() //Called via OnClick? event of a TabletButton.
    {
        taskManager.StartCurrentTask();
    }

    public void SkipTask() //Called via OnClick? event of a TabletButton.
    {
        taskManager.SkipCurrentTask();
    }

    public void ReturnToSkipped() //Called via OnClick? event of a TabletButton.
    {
        taskManager.ReturnToSkipped();
    }

    public void ActivateInstruction() //Called via OnClick? event of a TabletButton.
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
