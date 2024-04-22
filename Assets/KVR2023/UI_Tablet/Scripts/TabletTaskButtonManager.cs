using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TabletTaskButtonManager : MonoBehaviour
{
    public UnityEvent submitAssignment;
    public List<GameObject> buttons;
    [SerializeField] private GameObject beginButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private TaskManager taskManager;
    private readonly float desiredSpacing = .04f;
    private HorizontalLayoutGroup layoutGroup;

    private void Awake()
    {
        layoutGroup = gameObject.GetComponent<HorizontalLayoutGroup>();
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

    public void SubBeginPressed()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(true);
        }
        beginButton.SetActive(false);
        layoutGroup.spacing = desiredSpacing;
        taskManager.CurrentTask.CompleteEmptySub();
    }

    public void ResetSubPage()
    {
        foreach(GameObject button in buttons)
        {
            button.SetActive(false);
        }
        beginButton.SetActive(true);
        layoutGroup.spacing = 0f;
    }

    public void ResetParentPage()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
        continueButton.SetActive(true);
        layoutGroup.spacing = 0f;
        taskManager.SetContinueText();
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

    public void TryToSubmitAssignment()
    {
        bool canSubmit = taskManager.CanSubmitAssignment();
        if (canSubmit)
        {
            submitAssignment?.Invoke();
        }
    }
}
