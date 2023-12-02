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

    public void Update()
    {
        if (((Planet3.transform.position.x - planet3Pedestal.transform.position.x <= 4) && (planet3Pedestal.transform.position.x - Planet3.transform.position.x <= 4)) &&
            ((Planet3.transform.position.y - planet3Pedestal.transform.position.y <= 4) && (planet3Pedestal.transform.position.y - Planet3.transform.position.y <= 4)) &&
            ((Planet3.transform.position.z - planet3Pedestal.transform.position.z <= 4) && (planet3Pedestal.transform.position.z - Planet3.transform.position.z <= 4)) && IsActive)
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

