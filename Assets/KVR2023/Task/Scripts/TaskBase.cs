using System.Collections.Generic;
using UnityEngine;
using KVR2023;
using UnityEngine.Events;

namespace KVR2023
{
    public enum Criticality
    {
        Unskippable = 0,
        Skippable = 1,
        SubTaskDependent = 2
    }
}

public class TaskBase : MonoBehaviour
{
    public List<TaskBase> subs = new List<TaskBase>();
    [field: SerializeField] public string TaskName { get; set; }
    public int TaskID { get; set; }
    public bool IsSub { get; set; }
    public bool IsActive { get; set; }
    public bool IsComplete { get; set; }
    public bool completedCorrectly { get; set; }
    public int secondsToWaitBeforeAffordances;
    public UnityEvent affordances; //should contain link to a given affordance.Trigger()
    public UnityEvent stopAffordances; //the stop affordance event should contain link to the affordance.StopAffordance()
    public UnityEvent loadSubPage;
    public UnityEvent loadTestPage;
    public UnityEvent loadSandboxPage;
    public UnityEvent loadInstructionPage;
    public UnityEvent allSubsComplete;
    [field: SerializeField] public int Crit { get; set; }
    [SerializeField] protected TextUpdateManager textUpdateManager;
    [SerializeField] protected TaskManager taskManager;
    protected int subIndex;
    protected int numSubsComplete;
    protected TaskBase parentTask;
    public TaskBase CurrentSub { get; set; }
    protected string directions;

    public virtual void StartTask()
    {

    }
    public virtual void StopTask()
    {

    }

    public void SetSubValues(int subID, TaskBase task)
    {
        if (subs != null)
        {
            subID *= 100;
            foreach (TaskBase sub in subs)
            {
                if(sub.subs.Count > 0)
                {
                    Debug.LogError("Subtasks can not have their own subtasks at this time.");
                }
                sub.CurrentSub = null;
                sub.IsActive = false;
                sub.IsComplete = false;
                sub.parentTask = task;
                sub.subIndex = -1; //A value of -1 means that the task does not have subs.
                sub.IsSub = true;
                sub.TaskID = subID;
                subID++;
            }
        }
    }

    public void CompleteSub() //Only subs can invoke this.CompleteSub()
    {
        if(!IsSub)
        {
            Debug.LogError("Parent tasks can not invoke this.CompleteSub()");
        }
        else
        {
            IsComplete = true;
            IsActive = false;
            parentTask.numSubsComplete++;
            if (parentTask.AllSubsComplete())
            {
                parentTask.IsComplete = true;
                parentTask.IsActive = false;
                textUpdateManager.TriggerProgressTextUpdate("The current task has sub-tasks.  Press \"begin\" to start the first subtask.");
                textUpdateManager.TriggerTaskTextUpdate(" ");
                allSubsComplete?.Invoke(); //Reset Subs Page
                string currentMode = taskManager.CurrentMode;
                if(currentMode == "Sandbox") //Note: Consider refactoring such that currentMode is an int equal to the value in the enumeration, and this is a switch statement.
                {
                    parentTask.loadSandboxPage?.Invoke(); //Event only needs to be assigned in the parent tasks
                }
                else if(currentMode == "Instruction")
                {
                    parentTask.loadInstructionPage?.Invoke(); //Event only needs to be assigned in the parent tasks
                }
                else
                {
                    parentTask.loadTestPage?.Invoke(); //Event only needs to be assigned in the parent tasks
                }
                parentTask.taskManager.TaskCompletionUpdate();
            }
            else
            {
                parentTask.subIndex++;
                parentTask.CurrentSub = parentTask.subs[parentTask.subIndex];
                parentTask.CurrentSub.IsActive = true;
                parentTask.CurrentSub.StartTask();
                parentTask.CurrentSub.TextUpdateCurrentSub();
            }
        }
    }

    public void StartSubs() //When a parent task with subs is started, it needs to call this function.
    {
        if (IsSub)
        {
            Debug.LogError("StartSubs can not be invoked on a subtask.");
        }
        else if(subs.Count > 0)
        {
            subIndex = 0;
            CurrentSub = subs[subIndex];
            loadSubPage?.Invoke(); //Change Tablet Page to Subs Page
        }
    }

    public void CompleteEmptySub() //Called by TabletTaskButtonManager to automatically complete subtask 0 in a manner that hides it from the user.
    {
        CurrentSub.IsActive = false;
        CurrentSub.IsComplete = true;
        if(!AllSubsComplete())
        {
            subIndex++;
            CurrentSub = subs[subIndex];
            CurrentSub.StartTask();
            CurrentSub.TextUpdateCurrentSub();
        }
        else
        {
            Debug.LogError("The only subtask is the empty task.");
        }
    }

    protected bool AllSubsComplete()
    {
        foreach (TaskBase sub in subs)
        {
            if (!sub.IsComplete)
            {
                return false;
            }
        }
        return true;
    }

    protected void TextUpdateCurrentSub() //Called by other functions in the TaskManager class.  This is used to update the tablet text that displays current task information.
    {
        CurrentTaskTextUpdate currentTaskTextUpdate = new CurrentTaskTextUpdate(TaskName, TaskID, IsActive, IsComplete);
        string taskUpdate = currentTaskTextUpdate.ToString();
        textUpdateManager.TriggerTaskTextUpdate(taskUpdate);
    }

    protected void TextUpdateDirections()
    {
        textUpdateManager.TriggerProgressTextUpdate(directions);
    }

}
