using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events; // Required for UnityEvent

public class ActionHandler : MonoBehaviour
{
    public List<Transform> transformList;
    public GameObject prefabToInstantiate;
    public List<MaterialTrigger> materialTriggers; // List of MaterialTrigger objects
    public UnityEvent OnAllActionsCompleted; // Public UnityEvent

    private List<bool> actionsCompleted;
    private List<GameObject> instantiatedPrefabs;

    public int completedActionsCount { get; private set; }

    void Start()
    {
        actionsCompleted = new List<bool>();
        instantiatedPrefabs = new List<GameObject>();

        if (prefabToInstantiate != null)
        {
            foreach (Transform spawnPoint in transformList)
            {
                GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, spawnPoint.position, spawnPoint.rotation);
                instantiatedPrefabs.Add(instantiatedPrefab);
                actionsCompleted.Add(false);
            }
        }
        else
        {
            Debug.LogError("Prefab to instantiate is not assigned.");
        }

        completedActionsCount = 0;
    }

    // I had gemini re-write this method



    public void MarkActionAsCompleted(int actionIndex)
    {
        if (actionIndex >= 0 && actionIndex < actionsCompleted.Count)
        {
            // Special handling if it's the last action:
            if (actionIndex == actionsCompleted.Count - 1)
            {
                // Check if all other actions are already completed 
                bool allOthersCompleted = true;
                for (int i = 0; i < actionIndex; i++)
                {
                    if (!actionsCompleted[i])
                    {
                        allOthersCompleted = false;
                        break; // No need to continue the loop
                    }
                }

                if (allOthersCompleted)
                {
                    // ALL actions are complete, mark this one and invoke the event
                    actionsCompleted[actionIndex] = true;
                    completedActionsCount++;

                    // ... Rest of the code (destroying prefab, triggering material, etc.)

                    Debug.Log("All actions completed.");
                    OnAllActionsCompleted.Invoke(); // Invoke the event
                }
                else
                {
                    // It's the last action, but others are not complete. Do nothing. 
                    return;
                }
            }
            else
            {
                // Not the last action - process as normal
                if (!actionsCompleted[actionIndex])
                {
                    actionsCompleted[actionIndex] = true;
                    completedActionsCount++;

                    if (instantiatedPrefabs[actionIndex] != null)
                    {
                        Destroy(instantiatedPrefabs[actionIndex]);
                        Debug.Log("Prefab at index " + actionIndex + " destroyed.");
                    }

                    // Call TriggerMaterial on the corresponding MaterialTrigger
                    if (materialTriggers != null && actionIndex < materialTriggers.Count && materialTriggers[actionIndex] != null)
                    {
                        materialTriggers[actionIndex].TriggerMaterial();
                    }

                    Debug.Log("Action " + actionIndex + " marked as completed.");
                }
                else
                {
                    Debug.LogWarning("Action " + actionIndex + " is already marked as completed.");
                }
            }
        }
        else
        {
            Debug.LogError("Action index " + actionIndex + " is out of bounds.");
        }
    }



}
