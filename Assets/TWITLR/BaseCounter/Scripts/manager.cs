using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class manager : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay = 1.0f;
    public bool running = true;
   // public TMP_Text text;
   // public TMP_Text delayText;

    public void Start()
    {
       // delayText.text = delay.ToString("F2");
    }
    public void StartStop()
    {
      /*  if (text.text == "Start")
        {
            text.text = "Stop";
        } else
        {
            text.text = "Start";
        }*/
        running = !running;

    }
  /*  public void ExitApp()
    {
        Application.Quit();
    }*/

    public void Faster()
    {
        if (delay > 0.0f)
        {
            delay -= 0.1f;
        }
       // delayText.text = delay.ToString("F2");
    }
    public void Slower()
    {
        delay += 0.1f;
       // delayText.text = delay.ToString("F2");
    }
}
