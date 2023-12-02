using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugCatcher : MonoBehaviour
{
    public int keepXMessages = 5;
    int messages = 0;
   public TMP_Text output;

    // Start is called before the first frame update
    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }
    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        messages++;
        if (messages > keepXMessages)
        {
            output.text ="";
            messages = 0;
        }


        output.text += logString + "\n";
        if (type == LogType.Exception)
        {
           output.text += stackTrace + "\n";
        }
            
            // stack = stackTrace;
    }

    void Start()
    {
        output.text = "Output \n";
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
