using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSequencer : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToEnable;
    [SerializeField] private float interval = 1f;

    private int currentIndex = 0;

    private void Update()
    {
        if (Time.time >= nextSwitchTime)
        {
            SwitchObject();
        }
    }

    private float nextSwitchTime;

    private void SwitchObject()
    {
        // Disable current
        if (currentIndex < objectsToEnable.Count)
        {
            objectsToEnable[currentIndex].SetActive(false);
        }

        // Increment index
        currentIndex++;
        if (currentIndex >= objectsToEnable.Count)
        {
            currentIndex = 0;
        }

        // Enable next 
        objectsToEnable[currentIndex].SetActive(true);

        // Set next switch time
        nextSwitchTime = Time.time + interval;
    }
}