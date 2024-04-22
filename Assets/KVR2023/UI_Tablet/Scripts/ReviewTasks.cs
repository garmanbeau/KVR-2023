using System.Collections.Generic;
using TMPro;
using UnityEngine;
using KVR2023;
public class ReviewTasks : MonoBehaviour
{
    public TextMeshProUGUI textField;
    private TaskManager taskManager;

    private void Start()
    {
        // Find the TaskManager object in the scene
        taskManager = FindObjectOfType<TaskManager>();
        if (taskManager != null)
        {
            UpdateReview();
        }
        else
        {
            Debug.LogError("TaskManager object not found in the scene.");
        }
    }

    private void UpdateReview()
    {
        List<TaskBase> tasks = taskManager.tasks;
        string reviewText = "";
        int completedTasks = 0;

        foreach (TaskBase task in tasks)
        {
            // Skip tasks of type EmptyTask
            if (task is EmptyTaskZero)
            {
                continue;
            }

            // Build the review text with left-justified task name and right-justified Completed value
            string taskStatus = task.IsComplete ? "Completed" : "Not Completed";
            reviewText += $"{task.TaskName.PadRight(30)} {taskStatus.PadLeft(20)}\n";

            if (task.IsComplete)
            {
                completedTasks++;
            }
        }

        reviewText += $"Total Completed Tasks: {completedTasks.ToString().PadLeft(30)} / {tasks.Count}";

        textField.text = reviewText;
    }

}
