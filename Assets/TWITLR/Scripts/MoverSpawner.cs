using System.Collections.Generic;
using UnityEngine;

public class MoverSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;  // List of prefabs to spawn
    public Vector3 direction;         // Direction in which the spawned object will move
    public float spawnInterval;       // Time interval between spawns
    public float speed = 1;
    public float duration = 20;


    private float nextSpawnTime;      // Time when the next spawn will occur

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the next spawn time
        nextSpawnTime = Time.time + spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if it's time to spawn a new object
        if (Time.time >= nextSpawnTime)
        {
            // Update the next spawn time
            nextSpawnTime = Time.time + spawnInterval;

            // Choose a random prefab to spawn
            GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Count)];

            // Spawn the prefab at this object's position
            GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);


            // Randomly pick one of the major axes (X, Y, Z)
            Vector3 randomAxis = Vector3.zero;
            int axisChoice = Random.Range(0, 3);
            if (axisChoice == 0)
            {
                randomAxis = Vector3.right;
            }
            else if (axisChoice == 1)
            {
                randomAxis = Vector3.up;
            }
            else if (axisChoice == 2)
            {
                randomAxis = Vector3.forward;
            }

            // Randomly choose positive or negative direction
            if (Random.Range(0, 2) == 0)
            {
                randomAxis = -randomAxis;
            }

            // Align the prefab along the chosen axis
            spawnedObject.transform.forward = randomAxis;


            // Find the Mover script on the spawned object
            Mover mover = spawnedObject.GetComponent<Mover>();

            // Set the direction of the spawned object
            if (mover != null)
            {
                mover.direction = direction;
                mover.speed = speed;
                mover.duration = duration;
            }
        }
    }
}
