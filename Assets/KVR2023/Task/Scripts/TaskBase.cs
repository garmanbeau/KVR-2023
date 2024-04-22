using System.Collections.Generic;
using UnityEngine;
using KVR2023;
using UnityEngine.Events;
using System.Collections;

namespace KVR2023
{
    public enum Criticality
    {
        Unskippable = 0,
        Skippable = 1,
        SubTaskDependent = 2
    }
}

public abstract class TaskBase : MonoBehaviour
{
    public List<TaskBase> subs = new List<TaskBase>();
    [field: SerializeField] public string TaskName { get; set; }
    [field: SerializeField] public string TaskDescription { get; set; }
    public int TaskID { get; set; } //Primarily used to provide a numbering system for tasks to be displayed to the user.
    public bool IsSub { get; set; }
    [field: SerializeField] public bool IsActive { get; set; }
    [field: SerializeField] public bool IsComplete { get; set; }
    public TabletContext TabletContext { get; set; }
    [field: SerializeField] public bool WillPersist { get; set; }

    public int secondsToWaitBeforeAffordances;
    public UnityEvent affordances; //should contain link to a given affordance.Trigger()
    public UnityEvent stopAffordances; //the stop affordance event should contain link to the affordance.StopAffordance()
    public UnityEvent allSubsComplete;
    [field: SerializeField] public int Crit { get; set; }
    [SerializeField] protected TaskManager taskManager;
    [SerializeField] protected TabletPage subPage;
    protected int subIndex;
    protected int numSubsComplete;
    protected TaskBase parentTask;
    public TaskBase CurrentSub { get; set; }
    [SerializeField] protected string directions;
    

    public abstract void StartTask();

    public abstract void StopTask();

    public void SetSubValues(int subID, TaskBase task)
    {
        if (subs != null)
        {
            subID *= 100; //The subID passed in is equal to the parentID.
                          //A value of 100 is used such that when the Parent Task ID = 7,
                          //Sub ID = 700, 701, 702, etc.
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
                sub.TabletContext = FindObjectOfType<TabletContext>(); //Only works if there is only one instance of TabletContext.
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
            parentTask.StopAllCoroutines(); //calls the parent level and stops the coroutine at that level
            StopTask();//calls the stoptask method for the subtask.
            StopAllCoroutines();//ensures there are no ongoing coroutines
            stopAffordances?.Invoke(); //ensures that an affordance related to subtask does not continue, at subtask level
            parentTask.numSubsComplete++;
            if (parentTask.AllSubsComplete())
            {
                parentTask.IsComplete = true;
                parentTask.IsActive = false;
                subPage.UpdateText(1, "The current task has sub-tasks.  Press \"begin\" to start the first subtask."); //Reset Subs page
                subPage.UpdateText(2, " "); //Reset Subs page
                parentTask.allSubsComplete?.Invoke(); //Reset Subs Page. Event only needs to be assigned in the parent tasks.
                Pages currentMode = taskManager.CurrentMode;
                if (currentMode == Pages.Sandbox) //To Do: Consider that this if...else if...else structure is pointless as we already have the page we want to load.
                {
                    parentTask.TabletContext.LoadSpecificPage((int)Pages.Sandbox);
                }
                else if (currentMode == Pages.Instruction)
                {
                    parentTask.TabletContext.LoadSpecificPage((int)Pages.Instruction);
                }
                else
                {
                    parentTask.TabletContext.LoadSpecificPage((int)Pages.Test);
                }
                parentTask.taskManager.TaskCompletionUpdate(); //The parent task is now complete.
            }
            else
            {
                parentTask.subIndex++;
                parentTask.CurrentSub = parentTask.subs[parentTask.subIndex];
                parentTask.CurrentSub.IsActive = true;
                parentTask.CurrentSub.StartTask();

                if (taskManager.CurrentMode == Pages.Instruction || taskManager.CurrentMode == Pages.Sandbox)
                { 
                    parentTask.StartWaitForSubAffordanceTime(); //parent invokes the affordences for its subtasks.
                 }

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
            TabletContext.LoadSpecificPage((int)Pages.Subs); //Change Tablet Page to Subs Page
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

            if (taskManager.CurrentMode == Pages.Instruction || taskManager.CurrentMode == Pages.Sandbox)//Checks to make sure mode can recieve affordances
            {
                StartCoroutine(WaitForSubAffordanceTime()); //starts affordance countdown
            }
            CurrentSub.TextUpdateCurrentSub();
        }
        else
        {
            Debug.LogError("The only subtask is the empty task.");
        }
    }

    protected bool AllSubsComplete() //Self invoked after a subtask is complete to determine if there are more subtasks.
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

    protected void TextUpdateCurrentSub() //Called by other functions in TaskBase.  This is used to update the tablet text that displays current task information.
    {
        CurrentTaskTextUpdate currentTaskTextUpdate = new CurrentTaskTextUpdate(TaskName, TaskID, IsActive, IsComplete);
        string taskUpdate = currentTaskTextUpdate.ToString();
        subPage.UpdateText(2, taskUpdate);
    }

    protected void TextUpdateDirections()
    {
        subPage.UpdateText(1, directions); //To Do: Get rid of magic numbers.
    }

    public void StartWaitForSubAffordanceTime() //function call that allows a subtask, when complete, to start the next affordance. 
    {
        StartCoroutine(WaitForSubAffordanceTime());
    }
    private IEnumerator WaitForSubAffordanceTime() 
    {
        if (CurrentSub != null) { 
        yield return new WaitForSeconds(CurrentSub.secondsToWaitBeforeAffordances);
            CurrentSub.affordances?.Invoke();

        StartCoroutine(WaitForSubAffordanceTime());

        }
        yield break;
    }
}
