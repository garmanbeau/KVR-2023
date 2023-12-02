using UnityEngine;

public class TextUpdateManager : MonoBehaviour
{
    public TextUpdateEvent taskUpdateTextEvent = new TextUpdateEvent();
    public TextUpdateEvent taskProgressTextEvent = new TextUpdateEvent();

    // This method can be called whenever you want to update the current task text.
    public void TriggerTaskTextUpdate(string newText)
    {
        taskUpdateTextEvent.Invoke(newText);
    }
    // This method can be called whenever you want to update the task progress text.
    public void TriggerProgressTextUpdate(string newText)
    {
        taskProgressTextEvent.Invoke(newText);
    }
}
