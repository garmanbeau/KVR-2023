using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Created by William HP
 * Created on 07/13/2023
 * Followed guide at the following link: https://forum.unity.com/threads/ui-text-appearing-as-typing-one-letter-at-a-time.464340/
 */

public class DialogueWriter : MonoBehaviour
{
    public float delay = .01f;
    public string[] Correctlines;
    public string[] Wronglines;
    private string currentText = "";
    [HideInInspector]public int counter = 0;

    public void StartCorrectDialouge()
    { 
        StartCoroutine(WriteCorrectDialogue());
    }

    public void StartWrongDialogue()
    {
        StartCoroutine(WriteWrongAnswer());
    }

    public void EmptyWindow()
    {
        currentText = "";
        GetComponentInChildren<TextMeshProUGUI>().text = currentText;
    }

    IEnumerator WriteCorrectDialogue()
    {
        for(int i = 0; i < Correctlines[counter].Length; i++)
        {
            currentText = Correctlines[counter].Substring(0, i);
            GetComponentInChildren<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        string lastChar = Correctlines[counter].Substring(Correctlines[counter].Length - 1);
        Debug.Log("LastChar is" + lastChar);
        currentText += lastChar;
        GetComponentInChildren<TextMeshProUGUI>().text = currentText;
    }

    IEnumerator WriteWrongAnswer()
    {
        for (int i = 0; i < Wronglines[counter].Length; i++)
        {
            currentText = Wronglines[counter].Substring(0, i);
            GetComponentInChildren<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        string lastChar = Wronglines[counter].Substring(Wronglines[counter].Length - 1);
        Debug.Log("LastChar is" + lastChar);
        currentText += lastChar;
        GetComponentInChildren<TextMeshProUGUI>().text = currentText;
    }
}
