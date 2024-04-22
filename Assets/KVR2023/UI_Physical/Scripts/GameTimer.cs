using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public int time;
    public TextMeshProUGUI textComponent;
    public string timeString;
    public int tasksCompleted;

    public void StartTimer()
    {
        tasksCompleted = 0;
        time = 0;
        textComponent = GetComponent<TextMeshProUGUI>();
        StartCoroutine(TimeDelayCoroutine());
    }

    public void CallThreeTimes()
    {
        tasksCompleted += 1;
    }

    private IEnumerator TimeDelayCoroutine() 
    {
        yield return new WaitForSeconds(1f);
        time += 1;
        timeString = time.ToString();
        textComponent.text = timeString;
        if(tasksCompleted == 3)
        {
            yield break;
        }
        StartCoroutine(TimeDelayCoroutine());
    }
}
