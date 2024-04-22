using UnityEngine;
using System.Collections;

public class LidController : MonoBehaviour
{
    public float angle = 90.0f; // Rotation angle
    public float time = 1.0f; // Time to complete the rotation

    private bool isOpen = false; // Track the state of the lid

    // Method to toggle the lid state and start rotation
    public void ToggleLid()
    {
        // Determine the target rotation based on the current state
        float targetAngle = isOpen ? 0 : angle;
        StartCoroutine(RotateLid(targetAngle, time));
        isOpen = !isOpen; // Toggle the state
    }

    // Coroutine for smooth rotation
    IEnumerator RotateLid(float targetAngle, float duration)
    {
        // Store the current rotation of the gameObject
        Quaternion startRotation = transform.localRotation;
        // Calculate the target rotation based on the target angle
        Quaternion endRotation = Quaternion.Euler(targetAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);

        // Track the elapsed time
        float elapsedTime = 0;

        // Smoothly rotate from the start rotation to the end rotation over 'duration' seconds
        while (elapsedTime < duration)
        {
            transform.localRotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime; // Update the elapsed time
            yield return null; // Wait for the next frame
        }

        // Ensure the rotation is exactly the target rotation at the end
        transform.localRotation = endRotation;
    }
}
