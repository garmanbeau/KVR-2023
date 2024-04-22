using System;

/// <summary>
/// A class that is used to track when certain events occur.
/// While mostly used for recording when a task is started and completed, it can be used for almost any information recording.
/// <see cref="Test.LogEvent(string, string)"/> will record the information.
/// </summary>
/// <remarks> This class is necessary for Json serialization. JsonUtility package cannot take references to gameobjects directly and serialize them. You are left with
/// their instanceID and no other informtion. Newtonsoft.json does not cooperate well with unity classes due to self referencing issues and some depreciated rigid body logic.
/// Also, unlike <see cref="ObjectMetric"/>, this class does not have a Monobehavior counterpart because the time aspect is retrieved later and so a whole object is not being 
/// passed as a parameter.</remarks>
/// <seealso cref="InteractedObjectsDTO"/>
/// 
[Serializable] ///needs to be serializable so not to get a NullReference error when items are being added to list <see cref="Test.LogEvent(string, string)"/>
public class ActivityEventDTO
{    //cannot be properies becauase of issuses with serialization 
    public string taskName;
    public string eventType;
    public float timeSinceTestSelection;
}