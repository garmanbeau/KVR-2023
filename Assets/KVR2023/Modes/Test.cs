using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class Test : MonoBehaviour
{
    public InteractedObjectsDTO interactedObjects; //if not public, instance will not set.
    public UnityEvent testActivated;
    [SerializeField] private TaskManager taskManager;
    private bool modeIsActive;


    public void StartTask() //Called via UnityEvent by a button on the UI_Tablet test page.
    {
        taskManager.StartCurrentTask();
    }

    public void SkipTask() //Called via UnityEvent by a button on the UI_Tablet test page.
    {
        taskManager.SkipCurrentTask();
    }

    public void ReturnToSkipped()
    {
        taskManager.ReturnToSkipped();
    }

    public void ActivateTest() //Called via UnityEvent by a button on the UI_Tablet Mode Select Page.
    {
        bool canActivate = taskManager.ActivateTaskManager(2);
        if (canActivate)
        {
            testActivated?.Invoke();
        }
        else
        {
            Debug.LogError("The task manager is aleady active. You can't activate multiple modes at the same time.");
        }
    }



    /// <summary>
    /// Function typically called through an event in the scene (typically through OnPressed())
    /// to start the current task. 
    /// </summary>
    /// <param name="objectMetric">The information describing the grabbable object in the scene.</param>
    public void IsGrabbedObjectPartOfCurrentTask(ObjectMetric objectMetric)
    {
        if (modeIsActive)
        {
            bool isPartOfCurrentTask = false;
            Debug.Log("in test task");
            foreach (int associatedTaskId in objectMetric.AssociatedTaskIndexes)
            {
                if (taskManager.CurrentTask.TaskID == associatedTaskId)
                {
                    isPartOfCurrentTask = true;
                }
            }

            if (!isPartOfCurrentTask)
            {
                //code to populate List of ObjectMetric
                //TODO: add a time element to it;
                Debug.Log("Structure being saved is: " + objectMetric.SaveToString());

                ObjectMetricDTO objectMetricDto = new ObjectMetricDTO ///neccessary for serialization reasons, <see cref="ObjectMetricDTO"/>
                {
                    objectName = objectMetric.ObjectName,
                    description = objectMetric.Description,
                    associatedTaskIndexes = objectMetric.AssociatedTaskIndexes,
                };

                interactedObjects.incorrectlyGrabbedObjects.Add(objectMetricDto);

                Debug.Log("Object information successfully saved to list");
            }
        }
    }

    /// <summary>
    /// Helper Function to be used to save the information about the objects with which a student interacted
    /// </summary>
    /// TODO: Have called in "submit" functionality of Test. 
    public void SerializeInteractedObjectsToJSON()
    {
        var json = JsonUtility.ToJson(interactedObjects, true);
        Debug.Log(json);

        string path = Application.dataPath;
        System.IO.File.WriteAllText(path + "/StudentMetrics.json", json);
    }
}

/// <summary>
/// Class used to transer information about the objects a player interacted with. 
/// </summary>
/// <seealso cref="ObjectMetricDTO"/>


[Serializable]
public class InteractedObjectsDTO
{
    public List<ObjectMetricDTO> incorrectlyGrabbedObjects;
}

