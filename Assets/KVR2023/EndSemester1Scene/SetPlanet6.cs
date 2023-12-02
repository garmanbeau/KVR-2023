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

    public void Update()
    {
        if (((Planet6.transform.position.x - planet6Pedestal.transform.position.x <= 4) && (planet6Pedestal.transform.position.x - Planet6.transform.position.x <= 4)) &&
            ((Planet6.transform.position.y - planet6Pedestal.transform.position.y <= 4) && (planet6Pedestal.transform.position.y - Planet6.transform.position.y <= 4)) &&
            ((Planet6.transform.position.z - planet6Pedestal.transform.position.z <= 4) && (planet6Pedestal.transform.position.z - Planet6.transform.position.z <= 4)) && IsActive)
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

