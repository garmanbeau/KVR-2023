// 2024-01-04 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;
using System.Collections;
using TMPro;

public class ConsoleToText : MonoBehaviour
{

    public TMP_Text logText;
    public int messageLimit = 10;
    private int counter = 0;
    bool visibile = false;
    public bool showMSG = true;
    public bool showWAR = true;
    public bool showERR = true;

    // Make sure this GameObject is active in the scene
    void OnEnable()
    {
        Application.logMessageReceived += LogMessage;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogMessage;
    }

  public void ToggleVisibility()
    {
        visibile = !visibile;

        if (visibile)
        {
            logText.enabled = false;
        }
        else
        {
            logText.enabled = true;
        }
    }
    void LogMessage(string logString, string stackTrace, LogType type)
    {
        if (counter >= messageLimit)
        {
            logText.text = "";
            counter = 0;
        }

        switch (type)
        {
            case LogType.Error:
                if (showERR){
                    logText.text += "<color=red>" + logString + "</color>\n"; }
                break;
            case LogType.Warning:
                if (showWAR)
                {
                    logText.text += "<color=yellow>" + logString + "</color>\n";
                }
                break;
            default:if (showMSG)
                {
                    logText.text += logString + "\n";
                }
                break;
        }

        counter++;
    }
}
