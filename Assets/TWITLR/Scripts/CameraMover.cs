// 2024-02-05 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;
using System.Collections.Generic;

public class CameraMover : MonoBehaviour
{
    public List<Transform> transforms;
    public GameObject Camera;

    private int CurrentIndex = 0;

    private void Start()
    {
        MoveCameraToIndex(0);
    }

    public void MoveToNextTransform()
    {
        CurrentIndex = (CurrentIndex + 1) % transforms.Count;
        MoveCameraToIndex(CurrentIndex);
    }

    public void MoveToPreviousTransform()
    {
        if (CurrentIndex == 0)
        {
            CurrentIndex = transforms.Count - 1;
        }
        else
        {
            CurrentIndex--;
        }
        MoveCameraToIndex(CurrentIndex);
    }

    public void MoveToTransformAtIndex(int index)
    {
        if (index >= 0 && index < transforms.Count)
        {
            CurrentIndex = index;
            MoveCameraToIndex(CurrentIndex);
        }
    }

    private void MoveCameraToIndex(int index)
    {
        Camera.transform.position = transforms[index].position;
        Camera.transform.rotation = transforms[index].rotation;
    }
}
