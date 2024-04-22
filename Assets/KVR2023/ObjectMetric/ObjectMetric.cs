using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// A class that is to be put on objects in a scene that are used to complete.
/// If a user picks up an object not related to the current task in Test mode (determined by AssociatedTaskIndexes), 
/// <see cref="TestTaskManager.IsGrabbedObjectPartOfCurrentTask(ObjectMetric)"/> will record the information.
/// </summary>
public class ObjectMetric : MonoBehaviour
{
    [field: SerializeField] public string ObjectName { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public List<int> AssociatedTaskIndexes { get; private set; }

    /// <summary>
    /// Function that serializes an instance of ObjectMetric
    /// <remarks> Used primarily for debugging. Check references for all uses.</remarks>
    /// <returns> A json string with information about an instance of ObjectMetric.</returns>
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}

/// <summary>
/// Class that is used to transfer data relating to ObjectMetric through techniques like Json. 
/// </summary>
/// <remarks> This class is necessary for Json serialization. JsonUtility package cannot take references to gameobjects directly and serialize them. You are left with
/// their instanceID and no other informtion. Newtonsoft.json does not cooperate well with unity classes due to self referencing issues and some depreciated rigid body logic. </remarks>
/// <seealso cref="InteractedObjectsDTO"/>
/// 
[Serializable] ///needs to be serializable so not to get a NullReference error when items are being added to list <see cref="TestTaskManager.IsGrabbedObjectPartOfCurrentTask(ObjectMetric)"/>
public class ObjectMetricDTO
{    //cannot be properies becauase of issuses with serialization 
    public string objectName; //name of the object
    public string description; //description of the object
    public List<int> associatedTaskIndexes; //the tasks the object is used properly in.
    public string activeTaskWhileObjectGrabbed; // the name of the task that was active that didn't require the object grabbed
    public string activeTaskDescription; //description of that task.
}