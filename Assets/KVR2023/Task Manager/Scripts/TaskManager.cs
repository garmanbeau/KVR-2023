using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using KVR2023;

namespace KVR2023
{
    public enum Modes //Enumeration that holds the various modes.
    {
        Sandbox = 0,
        Instruction = 1,
        Test = 2
    }

    public struct CurrentTaskTextUpdate //Struct that holds the data sent when updating the current task text.  
    {
        private string taskName;
        private int taskID;
        private bool isComplete;
        private bool isActive;

        public CurrentTaskTextUpdate(string TaskName, int TaskID, bool IsActive, bool IsComplete) //Constructor called by TaskManager.TextUpdateCurrentTask.
        { //Primarily used for code readability.
            taskName = TaskName;
            taskID = TaskID;
            isActive = IsActive;
            isComplete = IsComplete;
        }

        public override string ToString() //Overriding the method so that we can format the contents of the text update.
        {
            return $"Task Name: {taskName}\nTask ID: {taskID}\nTask is Active: {isActive}\nTask is Complete: {isComplete}";
        }
    }

    public struct TaskProgressUpdate //Struct that holds the total number of tasks and uses it to format a string for task 
    {
        private int taskCount;
        private int numTasksComplete;

        public TaskProgressUpdate(int count, int completeCount)
        {
            taskCount = count;
            numTasksComplete = completeCount;
        }

        public override string ToString()
        {
            string cumulativeTaskString = "";
            int cumulativeTaskID = 0;
            for (int i = 0; i < taskCount; i++)
            {
                cumulativeTaskString += $"[{cumulativeTaskID}] ";
                cumulativeTaskID++;
            }

            return $"Total Task Count: {taskCount}\nTask Completion Count: {numTasksComplete}\n";
        }
    }
}

public class TaskManager : MonoBehaviour
{
    public List<TaskBase> tasks; //List of main tasks.
    public TaskBase CurrentTask { get; set; } //Reference to the current task.
    [SerializeField] private TextUpdateManager textUpdateManager; //Reference to The text update manager. Needed to update text on tablet pages.
    private string CurrentMode { get; set; } //Reference to the current mode.
    private int taskIndex; //Holds the index position of the current task in the list of tasks.
    [SerializeField] private Test test; //reference to test class so taskmanager can call SerializeInteractedObjectsToJSON()
    private bool hasSaved;
    private int numTasksComplete;


    void Start()
    {
        numTasksComplete = 0;
        CurrentMode = null;
        if (tasks != null && tasks.Count > 1) //If there are tasks stored in the list (besides the empty task).
        {
            CurrentTask = tasks[0]; //Set the current task to the first task in the list.
        }
        else
        {
            Debug.LogError("There are no tasks in the task manager's list of tasks");
        }
        int count = 0; //Define a local variable which is used in the loop to set the TaskID for each task and the taskIndex of the initialTask.
        foreach (TaskBase task in tasks)
        {
            task.TaskID = count;
            task.IsActive = false;
            task.IsComplete = false;
            task.IsSub = false;
            if (task.subs.Count > 0)
            {
                task.SetSubValues(count, task);
            }
            count++; //Increment indexCount before the next loop iteration.
        }

    }

    public bool ActivateTaskManager(int mode) //Called by functions in sandbox, instruction, or test classes.
    { //The purpose of this function is to set the CurrentMode of the task manager.
        if (CurrentMode == null)
        {
            switch ((Modes)mode)
            {
                case Modes.Sandbox:
                    {
                        CurrentMode = "Sandbox";
                        return true;
                    }
                case Modes.Instruction:
                    {
                        CurrentMode = "Instruction";
                        return true;
                    }
                case Modes.Test:
                    {
                        CurrentMode = "Test";
                        return true;
                    }
                default:
                    {
                        Debug.LogError("The value passed to TaskManager.ActivateTaskManager(int mode) is invalid.");
                        return false;
                    }
            }
        }
        else
        {
            return false;
        }
    }

    public void NextTask() //Called by function in sandbox class. Does not start the task.  This can be used for selecting a specific task in sandbox mode.
    { 
        if (taskIndex < tasks.Count - 1)
        {
            if (CurrentTask.IsActive)
            {
                CurrentTask.StopTask();
                CurrentTask.IsActive = false;
            }
            taskIndex++;
            CurrentTask = tasks[taskIndex];
            TextUpdateCurrentTask();
            TextUpdateProgress();
        }
        else
        {
            //Let the user know that there is not a next task.
        }
    }

    public void PreviousTask() //Called by function in sandbox class. Does not start the task.  This can be used for selecting a specific task in sandbox mode.
    {
        if (taskIndex > 1) //Now that we have an empty task 0, we don't let the user go to prev task unless they're on task 2 or higher.
        {
            if(CurrentTask.IsActive)
            {
                CurrentTask.StopTask();
                CurrentTask.IsActive = false;
            }
            taskIndex--;
            CurrentTask = tasks[taskIndex];
            TextUpdateCurrentTask();
            TextUpdateProgress();
        }
        else
        {
            //Let the user know that there is not a previous task.
        }
    }

    public void ReturnToSkipped()
    {
        foreach(TaskBase task in tasks)
        {
            if(task != null && !task.IsComplete && task.TaskID < CurrentTask.TaskID)
            {
                if (CurrentTask.IsActive)
                {
                    CurrentTask.StopTask();
                }
                taskIndex = task.TaskID;
                CurrentTask = task;
                TextUpdateCurrentTask();
            }
        }
    }

    public void SkipCurrentTask() //Called by functions in instruction and test classes. Currently skips by auto-completing the task.  We should probably add a value that lets the user know the task has been 
    {
        if(CurrentTask.IsComplete || CurrentTask.Crit == 1)
        {
            if(CurrentTask.IsActive)
            {
                CurrentTask.StopTask();
            }
            NextTask();
        }
        else //Note: The following logic overwrites the task text update.
        {
            string taskTextString = "Task number " + CurrentTask.TaskID + " can not be skipped.\nPress the start button to begin the task.";
            textUpdateManager.TriggerTaskTextUpdate(taskTextString);
        }
    }

    public void StartCurrentTask() //Called by functions in sandbox, instruction, or test classes.  This is used to start the current task.
    {
        if (CurrentTask != null && !CurrentTask.IsComplete)
        {
            CurrentTask.IsActive = true;
            CurrentTask.StartTask();
        }
        else //Note: The following logic overwrites the task text update.
        {
            string taskTextString = "Task " + CurrentTask.TaskID + " is already complete.";
            textUpdateManager.TriggerTaskTextUpdate(taskTextString);
        }
    }

    public void TaskCompletionUpdate() //This function must be called by Task Implementation classes when the task has been completed by the user.
    { //This text update is currently overwriting the current task text update.  
        CurrentTask.IsComplete = true;
        CurrentTask.IsActive = false;
        if (CurrentMode == "Test" && AreAllTasksComplete() && !hasSaved)
        {
            test.SerializeInteractedObjectsToJSON();
            hasSaved = true;
        }
        numTasksComplete++;
        string taskTextString = "Current Task: " + CurrentTask.TaskID + "\nIs Complete: " + CurrentTask.IsComplete;
        textUpdateManager.TriggerTaskTextUpdate(taskTextString);
        TextUpdateProgress();
        if (CurrentMode != "Sandbox")
        {
            NextTask();
        }
    }

    public bool CanSubmitAssignment()
    {
        foreach(TaskBase task in tasks)
        {
            if(!task.IsComplete && task.Crit != 1)
            {
                return false;
            }
        }
        return true;
    }

    private void TextUpdateCurrentTask() //Called by other functions in the TaskManager class.  This is used to update the tablet text that displays current task information.
    {
        CurrentTaskTextUpdate currentTaskTextUpdate = new CurrentTaskTextUpdate(CurrentTask.TaskName, CurrentTask.TaskID, CurrentTask.IsActive, CurrentTask.IsComplete);
        string taskUpdate = currentTaskTextUpdate.ToString();
        textUpdateManager.TriggerTaskTextUpdate(taskUpdate);
    }

    public void TextUpdateProgress() //Called by other functions in the TaskManager class and a function in the AssignmentComplete class.
    {

        TaskProgressUpdate taskProgressUpdate = new TaskProgressUpdate(tasks.Count - 1, numTasksComplete);
        string progressUpdate = taskProgressUpdate.ToString();
        textUpdateManager.TriggerProgressTextUpdate(progressUpdate);
    }

    private bool AreAllTasksComplete()
    {
        foreach (TaskBase task in tasks)
        {
            if (!task.IsComplete)
            {
                return false;
            }
        }
        return true;
    }
}
