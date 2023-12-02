using UnityEngine;

public class Mover : MonoBehaviour
{
    public Vector3 direction;  // Direction in which the object will move
    public float speed;         // Speed of the object
    public float duration;      // Time after which the object will be destroyed

    private float startTime;    // Time when the object was spawned

    // Start is called before the first frame update
    void Start()
    {
        // Record the start time
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        // Check if the object should be destroyed
        if (Time.time - startTime >= duration)
        {
            // Destroy the object
            Destroy(gameObject);
        }
    }
}
