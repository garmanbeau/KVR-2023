using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bit : MonoBehaviour
{
   public TMP_Text bitText;
    public int fieldValue = 1;
    public void SetOn()
    {
        bitText.text = "1";
    }

    public void SetOff()
    {
        bitText.text = "0";
    }

    public void ToggleBit()
    {
        if ( bitText.text == "0")  
        {
            bitText.text = "1";
        } else
        {
            bitText.text = "0";
        }
    }
    
  
}
