using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlanet1 : TaskBase
{
    /// <summary>
    /// This class is the implementation select the largest planet in the KVR2023 Sprint6Scene. 
    /// When the user grabs Jupiter in the scene, an event is triggered. 
    /// </summary>

    [SerializeField] private VisualAffordance VisualAffordance;
    [SerializeField] private GameObject planet1Pedestal;
    [SerializeField] private GameObject Planet1;

    public void Update()
    {
        if (((Planet1.transform.position.x - planet1Pedestal.transform.position.x <= 4) && (planet1Pedestal.transform.position.x - Planet1.transform.position.x <= 4)) &&
            ((Planet1.transform.position.y - planet1Pedestal.transform.position.y <= 4) && (planet1Pedestal.transform.position.y - Planet1.transform.position.y <= 4)) &&
            ((Planet1.transform.position.z - planet1Pedestal.transform.position.z <= 4) && (planet1Pedestal.transform.position.z - Planet1.transform.position.z <= 4)) && IsActive)
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

