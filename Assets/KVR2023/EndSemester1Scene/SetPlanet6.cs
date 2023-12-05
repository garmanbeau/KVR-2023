using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlanet6 : TaskBase
{
    /// <summary>
    /// This class is the implementation select the largest planet in the KVR2023 Sprint6Scene. 
    /// When the user grabs Jupiter in the scene, an event is triggered. 
    /// </summary>

    [SerializeField] private VisualAffordance VisualAffordance;
    [SerializeField] private GameObject planet6Pedestal;
    [SerializeField] private GameObject Planet6;

    private void Start()
    {
        directions = "Place the sixth planet in the solar system on platform six.";
    }

    public override void StartTask()
    {
        IsActive = true;
        TextUpdateDirections();
    }

    public override void StopTask()//nothing happens when stop task is called.
    {
        return;
    }


}

