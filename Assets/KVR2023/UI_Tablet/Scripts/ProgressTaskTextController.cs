using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressTaskTextController : MonoBehaviour
{
    [SerializeField] private TextUpdateManager textUpdateManager;
    private TextMeshProUGUI textComponent; // Reference to the TMP component on the monitor

    private void Start()
    {
        textComponent = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() // Subscribe to the event when the monitor is enabled
    {
        textUpdateManager.taskProgressTextEvent.AddListener(UpdateText);
    }

    private void OnDisable() // Unsubscribe when the monitor is disabled to prevent memory leaks
    {
        textUpdateManager.taskProgressTextEvent.RemoveListener(UpdateText);
    }

    private void UpdateText(string newText) // Update the TMP component with the new text
    {
        textComponent.text = newText;
    }
}
