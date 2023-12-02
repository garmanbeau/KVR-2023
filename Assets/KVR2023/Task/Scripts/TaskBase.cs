using System.Collections.Generic;
using UnityEngine;
using KVR2023;

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
    [field: SerializeField] public int TaskID { get; set; }
    [field: SerializeField] public bool IsSub { get; set; }
    public bool IsActive { get; set; }
    public bool IsComplete { get; set; }
    public bool completedCorrectly { get; set; }
    [field: SerializeField] public int Crit { get; set; }
    [SerializeField] protected TextUpdateManager textUpdateManager;
    [SerializeField] protected TaskManager taskManager;
    protected int subIndex;
    protected int numSubsComplete;
    protected TaskBase parentTask;

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
                if(sub.subs.Count != 0)
                {
                    Debug.LogError("Subtasks can not have their own subtasks at this time.");
                }
                sub.IsActive = false;
                sub.IsComplete = false;
                sub.parentTask = task;
                sub.subIndex = -1; //A value of negative 1 means that the task does not have subs.
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
                parentTask.taskManager.TaskCompletionUpdate(); //To Do: Pass the number of completed subs. 
            }
            else
            {
                parentTask.subIndex++;
                parentTask.subs[subIndex].IsActive = true;
                parentTask.subs[subIndex].StartTask();
            }
        }
    }

    public void StartSubs() //When a parent task with subs is started, it needs to call this function.
    {
        if(subs != null && subs.Count > 0)
        {
            subIndex = 0;
            subs[0].StartTask();
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
}
