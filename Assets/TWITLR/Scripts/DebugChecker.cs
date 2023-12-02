using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugChecker : MonoBehaviour
{
    public float Timeframe = 1.0f;
    public int buttonPressReq = 3;
    private float lastButtonPress = 0.0f;
    private int buttonPressed = 0;
    public HPQuizController controller;
    
    public void GoToDebug()
    {
        Debug.Log("I've been pressed " + buttonPressed + " times within the alloted time frame");
        if(buttonPressed == 0)
        {
            lastButtonPress = Time.time;
            buttonPressed++;
        }
        else if(Time.time - lastButtonPress < Timeframe)
        {
            lastButtonPress = Time.time;
            buttonPressed++;
            if(buttonPressed >= buttonPressReq)
            { controller.EnterDebug(); }
        }
        else
        { buttonPressed = 0; }
    }
}
