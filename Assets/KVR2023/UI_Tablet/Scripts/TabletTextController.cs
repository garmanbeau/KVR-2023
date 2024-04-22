using KVR2023;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KVR2023
{
    public enum TextUpdates
    {
        task = 0,
        progress = 1,
        header = 2,
        input = 3
    }
}

public class TabletTextController : MonoBehaviour
{
    private TextUpdateManager textUpdateManager;
    private TextMeshProUGUI textComponent; // Reference to the TMP component on the monitor
    [SerializeField] public int updateType;

    private void Start()
    {
        textComponent = gameObject.GetComponent<TextMeshProUGUI>();
        textUpdateManager = FindObjectOfType<TextUpdateManager>();
    }

    private void OnEnable() // Subscribe to the event when the monitor is enabled
    {
        switch(updateType)
        {
            case (int)TextUpdates.task:
            {
                textUpdateManager.taskUpdateTextEvent.AddListener(UpdateText);
                break;
            }
            case (int)TextUpdates.progress:
            {
                textUpdateManager.taskProgressTextEvent.AddListener(UpdateText);
                break;
            }
            case (int)TextUpdates.header:
            {
                textUpdateManager.headerTextEvent.AddListener(UpdateText);
                break;
            }
            case (int)TextUpdates.input:
            {
                textUpdateManager.inputTextEvent.AddListener(UpdateText);
                break;
            }
        }

    }

    private void OnDisable() // Unsubscribe when the monitor is disabled
    {
        switch (updateType)
        {
            case (int)TextUpdates.task:
                {
                    textUpdateManager.taskUpdateTextEvent.RemoveListener(UpdateText);
                    break;
                }
            case (int)TextUpdates.progress:
                {
                    textUpdateManager.taskProgressTextEvent.RemoveListener(UpdateText);
                    break;
                }
            case (int)TextUpdates.header:
                {
                    textUpdateManager.headerTextEvent.RemoveListener(UpdateText);
                    break;
                }
            case (int)TextUpdates.input:
                {
                    textUpdateManager.inputTextEvent.RemoveListener(UpdateText);
                    break;
                }
        }
    }

    private void UpdateText(string newText) // Update the TMP component with the new text
    {
        textComponent.text = newText;
    }
}
