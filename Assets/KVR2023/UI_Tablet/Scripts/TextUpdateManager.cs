using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUpdateManager : MonoBehaviour
{
    public TextUpdateEvent taskUpdateTextEvent = new TextUpdateEvent();
    public TextUpdateEvent taskProgressTextEvent = new TextUpdateEvent();
    public TextUpdateEvent headerTextEvent = new TextUpdateEvent();
    public TextUpdateEvent inputTextEvent = new TextUpdateEvent();
    public string taskString { get; private set; }
    public string progressString { get; private set; }
    public string headerString { get; private set; }
    public string inputString { get; private set; }

    public void TriggerTaskTextUpdate(string newText)
    {
        taskUpdateTextEvent.Invoke(newText);
        taskString = newText;
    }
    public void TriggerProgressTextUpdate(string newText)
    {
        taskProgressTextEvent.Invoke(newText);
        progressString = newText;
    }
    public void TriggerHeaderTextUpdate(string newText)
    {
        headerTextEvent.Invoke(newText);
        headerString = newText;
    }
    public void TriggerInputTextUpdate(string newText)
    {
        inputTextEvent.Invoke(newText);
        inputString = newText;
    }
}
