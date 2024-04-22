using UnityEngine;
using System.Collections.Generic;

public class BatteryReceiver : MonoBehaviour
{
    public List<GameObject> gameObjectPrefabs;
    public GameObject visibleObject;


    public void DestroyVisibleObject()
    {
        if (visibleObject != null)
        {
            Destroy(visibleObject);
            visibleObject = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BATTERY"))
        {
            // Check if visibleObject is not null and destroy it
            if (visibleObject != null)
            {
                Destroy(visibleObject);
                visibleObject = null;
            }

            InstructionBlock instructionBlock = other.gameObject.GetComponent<InstructionBlock>();
            if (instructionBlock != null && instructionBlock.prefabIndex < gameObjectPrefabs.Count)
            {
                int prefabIndex = instructionBlock.prefabIndex;
                GameObject instantiatedPrefab = Instantiate(gameObjectPrefabs[prefabIndex], transform.position, transform.rotation, transform);
                visibleObject = instantiatedPrefab;
                Destroy(other.gameObject);
            }
        }
    }
}
