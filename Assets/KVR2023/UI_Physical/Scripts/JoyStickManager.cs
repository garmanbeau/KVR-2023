using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickManager : MonoBehaviour
{
    public GameObject start;
    public GameObject mode;
    public GameObject task;

    private void Start()
    {
        start.SetActive(true);
        mode.SetActive(false);
        task.SetActive(false);
    }

    public void StartUnpressed()
    {
        start.SetActive(false);
        mode.SetActive(true);
    }

    public void ModeUnpressed()
    {
        mode.SetActive(false);
        task.SetActive(true);
    }
}
