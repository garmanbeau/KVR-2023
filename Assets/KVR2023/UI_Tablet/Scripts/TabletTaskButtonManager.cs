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
    [SerializeField] private TaskManager taskManager;
    private float desiredSpacing = .04f;

    private void Awake()
    {
        gameObject.GetComponent<HorizontalLayoutGroup>().spacing = 0f;
    }

    public void BeginPressed()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(true);
        }
        beginButton.SetActive(false);
        gameObject.GetComponent<HorizontalLayoutGroup>().spacing = desiredSpacing;
        taskManager.CurrentTask.IsComplete = true;
        taskManager.CurrentTask.IsActive = false;
        taskManager.NextTask();
    }

    public void TryToSubmitAssignment()
    {
        bool canSubmit = taskManager.CanSubmitAssignment();
        if (canSubmit)
        {
            submitAssignment?.Invoke();
        }
        else
        {
            //Eventually add something to make the button red.
        }
    }
}
