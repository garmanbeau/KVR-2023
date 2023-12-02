using UnityEngine;
using UnityEngine.Events;

public class TextTaskUpdateStartup : MonoBehaviour
{
    public UnityEvent updateTextTaskUpdate;
    // Start is called before the first frame update
   
    /// <summary>
    /// Called on first frame of activation. the invoke method is primarily used to call <see cref="TaskManager.SafeUpdateCurrentTask"/>
    /// so that the first task page does not need to be manually set.
    /// </summary>
    void Start()
    {
        updateTextTaskUpdate.Invoke();
    }
}
