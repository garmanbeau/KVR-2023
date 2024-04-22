using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using KVR2023;
using System.Text;
using Defective.JSON;

public class NetworkRequestManager : MonoBehaviour
{
    private static string baseURL = "Placeholder"; //This will be replaced with the url when we're ready to begin proper testing.

    public void UpdateWebServer(string playerName, string playerKey, InteractedObjectsDTO interactedObjects)
    {
        string url = baseURL;

        JSONObject rootObject = new JSONObject();
        JSONObject incorrectlyGrabbedObjects = new JSONObject();
        foreach (var metric in interactedObjects.incorrectlyGrabbedObjects)
        {
            JSONObject objectMetric = new JSONObject();
            objectMetric.AddField("objectName", metric.objectName);
            objectMetric.AddField("description", metric.objectName);
            JSONObject associatedTaskIndexes = new JSONObject();
            foreach(var index in metric.associatedTaskIndexes)
            {
                associatedTaskIndexes.AddField("associatedTaskIndex", index);
            }
            objectMetric.AddField("associatedTaskIndexes", associatedTaskIndexes);
            objectMetric.AddField("activeTaskWhileObjectGrabbed", metric.activeTaskWhileObjectGrabbed);
            objectMetric.AddField("activeTaskDescription", metric.activeTaskDescription);
            incorrectlyGrabbedObjects.AddField("objectMetric", objectMetric);
        }
        JSONObject eventLogs = new JSONObject();
        foreach (var log in interactedObjects.eventLog)
        {
            JSONObject eventLog = new JSONObject();
            eventLog.AddField("taskName", log.taskName);
            eventLog.AddField("eventType", log.eventType);
            eventLog.AddField("timeSinceTestSelection", log.timeSinceTestSelection);
            eventLogs.AddField("eventLog", eventLog);
        }
        rootObject.AddField("playerName", playerName);
        rootObject.AddField("playerKey", playerKey);
        rootObject.AddField("incorrectlyGrabbedObjects", incorrectlyGrabbedObjects);
        rootObject.AddField("eventLogs", eventLogs);
        string json = rootObject.ToString();
        Debug.Log(json);
        //StartCoroutine(PostRequestCoroutine(url, json));
    }

    private IEnumerator PostRequestCoroutine(string url, string json)
    {
        using (var request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {

                string serverLog = request.downloadHandler.text;
                Debug.Log(serverLog);
                //To Do: Decide if this data is going to be presented to the user.
            }
            else
            {
                string serverLog = request.downloadHandler.text;
                Debug.Log(serverLog);
                //To Do: Decide if this data is going to be presented to the user
            }
        }
    }
}
