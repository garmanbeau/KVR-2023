// 2024-02-02 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabInstance;

    void Start()
    {
        SpawnPrefab();
    }

    public void SpawnPrefab()
    {
        StartCoroutine(SpawnWithDelay());
    }

    IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(2);
        GameObject newPrefab = Instantiate(prefabInstance, transform.position, transform.rotation);
        SpawnedPrefab prefabScript = newPrefab.GetComponent<SpawnedPrefab>();
        if (prefabScript != null)
        {
            prefabScript.MySpawner = this;
        }
    }
}
