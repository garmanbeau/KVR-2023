using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using KVR2023;
using UnityEngine.Events;

namespace KVR2023
{
    public struct CurrentTaskTextUpdate //Struct that holds the data sent when updating the current task text.  
    {
        private string taskName;
        private int taskID;
        private bool isComplete;
        private bool isActive;

        public CurrentTaskTextUpdate(string TaskName, int TaskID, bool IsActive, bool IsComplete) //Constructor called by TaskManager and TaskBase functions
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
    public static TaskManager instance = null;
    public List<TaskBase> tasks; //List of main tasks.
    [field: SerializeField] public TaskBase CurrentTask { get; set; } //Reference to the current task.
    [field: SerializeField] public bool WillPersistToNextScene { get; set; }
    [field: SerializeField] public bool IsOriginalScene { get; set; }
    [field: SerializeField] private bool IsSecondPass { get; set; }//when loading new scenes it sometimes calles OnloadedScene Twice

    private int taskIndex; //Holds the index position of the current task in the list of tasks.
    [SerializeField] private Sandbox sandbox; //Reference to sandbox class, used to trigger textUpdates.
    [SerializeField] private Instruction instruction; //Reference to instruction class, used to trigger textUpdates.
    [SerializeField] private Test test; //reference to test class so taskmanager can call SerializeInteractedObjectsToJSON()
    private bool hasSaved;
    private int numTasksComplete;
    public Pages CurrentMode { get; set; }

    void Awake()
    {
        // If an instance already exists and it's not this:
        if (instance != null && instance != this)
        {
            // Then destroy the other instance. This enforces our singleton pattern, meaning there can only ever be one instance of a TaskManager.
            Destroy(instance.tasks[0].transform.root.gameObject); // destroys parent object if tasks are in an empty game object
            foreach (TaskBase task in instance.tasks)
            {
                Destroy(task.gameObject); // Destroy the task GameObject
            }
        Destroy(instance.gameObject);//destroy the game object this instance has
        }

        // Set this as the new instance
        instance = this;

        // Other code from your Awake() method...
        MakePersistent();
    }

    void Start()
    {
        numTasksComplete = 0;
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
            task.TabletContext = FindObjectOfType<TabletContext>();
            if (task.subs.Count > 0)
            {
                task.SetSubValues(count, task);
            }
            count++; //Increment indexCount before the next loop iteration.
        }

        //find mode object in scene and activate task manager based on mode object. 
        // Find the modeObject in the scene
        GameObject modeObject = GameObject.Find("modeObject");

        // If modeObject is not null
        if (modeObject != null)
        {
            // Get the mode from the modeObject
            // This assumes that modeObject has a Mode component that stores the mode
            string mode = modeObject.GetComponent<Mode>().ModeName;

            // Switch case statement based on mode
            switch (mode)
            {
                case "Sandbox":
                    // Activate Sandbox case
                    sandbox.ActivateSandbox();
                    CurrentTask.IsComplete = true;
                    CurrentTask.IsActive = false;
                    NextTask();
                    Debug.Log("Sandbox mode activated");
                    break;

                case "Instruction":
                    // Activate Instruction case
                    instruction.ActivateInstruction();
                    CurrentTask.IsComplete = true;
                    CurrentTask.IsActive = false;
                    NextTask();
                    Debug.Log("Instruction mode activated");
                    break;

                case "Test":
                    // Activate Test case
                    test.ActivateTest();
                    CurrentTask.IsComplete = true;
                    CurrentTask.IsActive = false;
                    NextTask();
                    Debug.Log("Test mode activated");
                    break;

                default:
                    Debug.Log("Unknown mode");
                    break;
            }
        }
        else
        {
            Debug.Log("modeObject not found");
        }


    }

    public bool ActivateTaskManager(Pages newPage) //Called by functions in sandbox, instruction, or test classes.
    { //The purpose of this function is to set the CurrentMode of the task manager.
        if (CurrentMode == Pages.Start) //CurrentMode is an enumeration, so it defaults to the first value, which is the StartPage.
        {
            CurrentMode = newPage;
            return true;
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
            if (CurrentTask.IsActive)
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
        foreach (TaskBase task in tasks)
        {
            if (task != null && !task.IsComplete && task.TaskID < CurrentTask.TaskID)
            {
                if (CurrentTask.IsActive)
                {
                    CurrentTask.StopTask();
                    StopAllCoroutines();//ensures there are no ongoing affordances
                }
                taskIndex = task.TaskID;
                CurrentTask = task;
                TextUpdateCurrentTask();
            }
        }
    }

    public void SkipCurrentTask() //Called by functions in instruction and test classes. Currently skips by auto-completing the task.  We should probably add a value that lets the user know the task has been 
    {
        if (CurrentTask.IsComplete || CurrentTask.Crit == 1)
        {
            if (CurrentTask.IsActive)
            {
                CurrentTask.StopTask();
                StopAllCoroutines();//ensures there are no ongoing affordances
            }
            NextTask();
        }
        else //Note: The following logic overwrites the task text update.
        {
            string taskTextString = "Task number " + CurrentTask.TaskID + " can not be skipped.\nPress the start button to begin the task.";
            switch (CurrentMode)
            {
                case Pages.Sandbox:
                    {
                        sandbox.UpdateText(1, taskTextString); //To Do: Replace magic number.
                        break;
                    }
                case Pages.Instruction:
                    {
                        instruction.UpdateText(1, taskTextString); //To Do: Replace magic number.
                        break;
                    }
                case Pages.Test:
                    {
                        test.UpdateText(1, taskTextString); //To Do: Replace magic number.
                        break;
                    }
            }
        }
    }

    public void StartCurrentTask() //Called by functions in sandbox, instruction, or test classes.  This is used to start the current task.
    {
        if (CurrentTask != null && !CurrentTask.IsComplete)
        {
            if (CurrentTask.subs.Count > 0)
            {
                switch (CurrentMode)
                {
                    case Pages.Sandbox:
                        {
                            sandbox.ResetPage();
                            break;
                        }
                    case Pages.Instruction:
                        {
                            instruction.ResetPage();
                            break;
                        }
                    case Pages.Test:
                        {
                            test.ResetPage();
                            break;
                        }
                }
            }
            CurrentTask.IsActive = true;
            CurrentTask.StartTask();
            // add time stamp
            if (CurrentMode == Pages.Test)
            {
                test.LogEvent(CurrentTask.name, "Started Task");
            }
            else if (CurrentMode == Pages.Instruction || CurrentMode == Pages.Sandbox)
            {
                StartCoroutine(WaitForAffordanceTime()); //Waits for x amount of specified time before triggering the affordance coroutine.
            }
        }
        else //Note: The following logic overwrites the task text update.
        {
            string taskTextString = "Task " + CurrentTask.TaskID + " is already complete.";
            CustomTaskTextUpdate(taskTextString);
        }
    }

    public void StartSpecificTask(int taskToStartIndex) //Called by functions in sandbox, instruction, or test classes.  This is used to start the current task.
    {

        if (taskToStartIndex < tasks.Count && taskToStartIndex > 0) //ensures taskIndex is valid
        {
            if (CurrentTask.IsActive) //stops the current task before moving on
            {
                CurrentTask.StopTask();
                CurrentTask.IsActive = false;
            }

            CurrentTask = tasks[taskToStartIndex]; //sets current task
            taskIndex = taskToStartIndex;
            TextUpdateCurrentTask();
            TextUpdateProgress();

        }

        if (CurrentTask != null && !CurrentTask.IsComplete)
        {
            if (CurrentTask.subs.Count > 0)
            {
                tabletTaskButtonManager.ResetParentPage();
            }
            CurrentTask.IsActive = true;
            CurrentTask.StartTask();

            // add time stamp
            if (CurrentMode == Pages.Test)
            {
                test.LogEvent(CurrentTask.name, "Started Task");
            }
            else if (CurrentMode == Pages.Instruction || CurrentMode == Pages.Sandbox)
            {
                StartCoroutine(WaitForAffordanceTime()); //Waits for x amount of specified time before triggering the affordance coroutine.
            }
        }
        else //Note: The following logic overwrites the task text update.
        {
            string taskTextString = "Task " + CurrentTask.TaskID + " is already complete.";
            CustomTaskTextUpdate(taskTextString);
        }
    }

    //This function must be called by Task Implementation classes when the task has been completed by the user.
    //The parameter forceNextTask can be used when in sandbox mode but you want to call NextTask()
    public void TaskCompletionUpdate(bool forceNextTask = false) 
    { //This text update is currently overwriting the current task text update.  
        if (!CurrentTask.IsActive) //the task must be active before it can be completed
        {
            return;
        }
        CurrentTask.IsComplete = true;
        if (CurrentMode == Pages.Test) //add time stamp
        {
            test.LogEvent(CurrentTask.name, "Completed Task");
        }
        CurrentTask.IsActive = false;
        CurrentTask.StopTask(); //ensures there are no ongoing affordances
        StopAllCoroutines(); //ensures there are no ongoing affordances
        if (CurrentMode == Pages.Test && AreAllTasksComplete() && !hasSaved)
        {
            test.SerializeInteractedObjectsToJSON();
            test.InitiateNetworkRequest();
            hasSaved = true;
        }
        numTasksComplete++;
        string taskTextString = "Current Task: " + CurrentTask.TaskID + "\nIs Complete: " + CurrentTask.IsComplete;
        CustomTaskTextUpdate(taskTextString);
        TextUpdateProgress();
        if (CurrentMode != Pages.Sandbox || forceNextTask)
        {
            NextTask();
        }
    }

    public void SafeTextUpdate()
    {
        if (CurrentTask != null && CurrentTask != tasks[0])
        {
            TextUpdateCurrentTask();
            TextUpdateProgress();
        }
    }

    public void SetContinueText() //Invoked by continue button on tablet mode pages.
    {
        string newString = "Sub Tasks Complete.  Press continue to proceed with main tasks.";
        CustomTaskTextUpdate(newString);
        newString = " ";
        CustomProgressTextUpdate(newString);
    }

    public bool CanSubmitAssignment()
    {
        foreach (TaskBase task in tasks)
        {
            if (!task.IsComplete && task.Crit != 1)
            {
                return false;
            }
        }
        return true;
    }

    public void CustomTaskTextUpdate(string newText)
    {
        switch (CurrentMode)
        {
            case Pages.Sandbox:
                {
                    sandbox.UpdateText(1, newText); //To Do: Replace magic number.
                    break;
                }
            case Pages.Instruction:
                {
                    instruction.UpdateText(1, newText); //To Do: Replace magic number.
                    break;
                }
            case Pages.Test:
                {
                    test.UpdateText(1, newText); //To Do: Replace magic number.
                    break;
                }
        }
    }

    public void CustomProgressTextUpdate(string newText)
    {
        switch (CurrentMode)
        {
            case Pages.Sandbox:
                {
                    sandbox.UpdateText(2, newText); //To Do: Replace magic number.
                    break;
                }
            case Pages.Instruction:
                {
                    instruction.UpdateText(2, newText); //To Do: Replace magic number.
                    break;
                }
            case Pages.Test:
                {
                    test.UpdateText(2, newText); //To Do: Replace magic number.
                    break;
                }
        }
    }

    private void TextUpdateCurrentTask() //Called by other functions in the TaskManager class.  This is used to update the tablet text that displays current task information.
    {
        CurrentTaskTextUpdate currentTaskTextUpdate = new CurrentTaskTextUpdate(CurrentTask.TaskName, CurrentTask.TaskID, CurrentTask.IsActive, CurrentTask.IsComplete);
        string taskUpdate = currentTaskTextUpdate.ToString();
        switch (CurrentMode)
        {
            case Pages.Sandbox:
                {
                    sandbox.UpdateText(1, taskUpdate); //To Do: Replace magic number.
                    break;
                }
            case Pages.Instruction:
                {
                    instruction.UpdateText(1, taskUpdate); //To Do: Replace magic number.
                    break;
                }
            case Pages.Test:
                {
                    test.UpdateText(1, taskUpdate); //To Do: Replace magic number.
                    break;
                }
        }
    }

    public void TextUpdateProgress() //Called by other functions in the TaskManager class and a function in the AssignmentComplete class.
    {
        string progressUpdate = this.GetProgressString();
        switch (CurrentMode)
        {
            case Pages.Sandbox:
                {
                    sandbox.UpdateText(2, progressUpdate); //To Do: Replace magic number.
                    break;
                }
            case Pages.Instruction:
                {
                    instruction.UpdateText(2, progressUpdate); //To Do: Replace magic number.
                    break;
                }
            case Pages.Test:
                {
                    test.UpdateText(2, progressUpdate); //To Do: Replace magic number.
                    break;
                }
        }
    }

    public string GetProgressString()
    {
        TaskProgressUpdate taskProgressUpdate = new TaskProgressUpdate(tasks.Count - 1, numTasksComplete);
        string progressUpdate = taskProgressUpdate.ToString();
        return progressUpdate;
    }

    private void MakePersistent()
    {
        Debug.Log("MakePersistent");

        if (WillPersistToNextScene)
        {
            DontDestroyOnLoad(gameObject); // Persist the parent GameObject TaskManager. The TaskManager gameobject cannot be a child of another gameobject

            SceneManager.sceneLoaded += OnSceneLoaded;
            // Iterate through each TaskBase object and make them persistent
            foreach (TaskBase task in tasks)
            {
                if (task != null) // Ensure the task is not null
                {
                    DontDestroyOnLoad(task.transform.root.gameObject);//gets the encompassing game object and persists that
                    DontDestroyOnLoad(task.gameObject); // persist the game object that the taskbase is attached to
                    task.WillPersist = true;
                    Debug.Log($"Task '{task.name}' is persistent.");
                }
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("OnSceneLoaded");

        if (WillPersistToNextScene && IsOriginalScene)
        {
            IsOriginalScene = false; ;
        }
        else if (WillPersistToNextScene && !IsOriginalScene)
        {
            WillPersistToNextScene = false;
            foreach (TaskBase task in tasks)
            {
                if (task != null) // Ensure the task is not null
                {
                    task.WillPersist = false;
                    Debug.Log($"Task '{task.name}' will not persist to the next scene.");
                }
            }

        }
        else if (!WillPersistToNextScene && !IsOriginalScene)
        {
            CheckAndDestroy();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }


    }

    private void CheckAndDestroy()
    {
        Debug.Log("CheckAndDestroy");
        Destroy(tasks[0].transform.root.gameObject); // destroys parent object if tasks are in an empty game object
        foreach (TaskBase task in tasks)
        {
            if (task != null && !task.WillPersist) // Ensure the task is not null and the flag is not set
            {
                Debug.Log($"Task '{task.name}' will be destroyed.");
                Destroy(task.gameObject); // Destroy the task GameObject
            }
        }
        Destroy(gameObject);//destroy the game object this instance has
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

    //Waits for x amount of specified time before triggering the affordance coroutine.
    private IEnumerator WaitForAffordanceTime()
    {
        if (CurrentTask != null)
        {
            //TaskBase inputTask = CurrentTask;
            yield return new WaitForSeconds(CurrentTask.secondsToWaitBeforeAffordances);
            CurrentTask.affordances?.Invoke();

            StartCoroutine(WaitForAffordanceTime());
        }
        yield break;
    }
}
