using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class VisualAffordance : MonoBehaviour, IAffordance
{
    [SerializeField]
    private GameObject objectToFlash;

    private Renderer objectRenderer;

    private void Start()
    {
        if (objectToFlash != null)
        {
            objectRenderer = objectToFlash.GetComponent<Renderer>();
        }
    }

    public void Trigger()
    {
        if (objectToFlash == null || objectRenderer == null)
        {
            Debug.LogError("Object to flash or its renderer is not assigned.");
            return;
        }

        if (objectRenderer.enabled)
        {
            StartCoroutine(FlashHint());
        }
    }

    public void StopAffordance()
    {
        StopAllCoroutines();
        objectRenderer.enabled = true;
    }
    private IEnumerator FlashHint()
    {
        float flashDuration = 10f;
        float flashInterval = 1f;
        bool isVisible = true;

        while (flashDuration > 0f)
        {
            objectRenderer.enabled = isVisible;
            isVisible = !isVisible;
            yield return new WaitForSeconds(flashInterval);
            flashDuration -= flashInterval;
        }

        objectRenderer.enabled = true; // Ensure the renderer is visible after the coroutine ends
    }
}
