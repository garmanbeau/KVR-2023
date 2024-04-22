using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PartSet : MonoBehaviour
{
    // Public list of GameObjects
    public List<GameObject> gameObjects;

    // Public UnityEvent
    public UnityEvent OnSetComplete;
    bool setComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        // Disable all objects in the list
        foreach (var gameObject in gameObjects)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if all objects are active
        if (AreAllObjectsActive() && !setComplete)
        {
            setComplete = true;
            // Invoke OnSetComplete event
            OnSetComplete.Invoke();
        }
    }

    // Helper method to check if all objects are active
    private bool AreAllObjectsActive()
    {
        foreach (var gameObject in gameObjects)
        {
            if (gameObject == null || !gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
