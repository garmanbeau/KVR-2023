using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlanet5 : TaskBase
{
    /// <summary>
    /// This class is the implementation select the largest planet in the KVR2023 Sprint6Scene. 
    /// When the user grabs Jupiter in the scene, an event is triggered. 
    /// </summary>

    [SerializeField] private VisualAffordance VisualAffordance;
    [SerializeField] private GameObject planet5Pedestal;
    [SerializeField] private GameObject Planet5;

    public void Update()
    {
        if (((Planet5.transform.position.x - planet5Pedestal.transform.position.x <= 4) && (planet5Pedestal.transform.position.x - Planet5.transform.position.x <= 4)) &&
            ((Planet5.transform.position.y - planet5Pedestal.transform.position.y <= 4) && (planet5Pedestal.transform.position.y - Planet5.transform.position.y <= 4)) &&
            ((Planet5.transform.position.z - planet5Pedestal.transform.position.z <= 4) && (planet5Pedestal.transform.position.z - Planet5.transform.position.z <= 4)) && IsActive)
        {
            this.CompleteSub();
        }
    }

    public override void StartTask()
    {
        IsActive = true;
    }

    public override void StopTask()//nothing happens when stop task is called.
    {
        return;
    }


}

