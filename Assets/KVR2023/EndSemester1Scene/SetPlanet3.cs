using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlanet3 : TaskBase
{
    /// <summary>
    /// This class is the implementation select the largest planet in the KVR2023 Sprint6Scene. 
    /// When the user grabs Jupiter in the scene, an event is triggered. 
    /// </summary>

    [SerializeField] private VisualAffordance VisualAffordance;
    [SerializeField] private GameObject planet3Pedestal;
    [SerializeField] private GameObject Planet3;

    private void Start()
    {
        directions = "Place the third planet in the solar system on platform three.";
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

