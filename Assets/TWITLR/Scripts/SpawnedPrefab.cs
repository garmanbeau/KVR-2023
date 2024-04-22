// 2024-02-02 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class SpawnedPrefab: MonoBehaviour
{
    public Spawner MySpawner { get; set; }

    // a method in PrefabScript that calls the SpawnPrefab method of the spawner
    public void CallSpawnPrefab()
    {
        MySpawner.SpawnPrefab();
    }
}
