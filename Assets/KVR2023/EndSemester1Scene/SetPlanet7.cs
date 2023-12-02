using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlanet7 : TaskBase
{
    /// <summary>
    /// This class is the implementation select the largest planet in the KVR2023 Sprint6Scene. 
    /// When the user grabs Jupiter in the scene, an event is triggered. 
    /// </summary>

    [SerializeField] private VisualAffordance VisualAffordance;
    [SerializeField] private GameObject planet7Pedestal;
    [SerializeField] private GameObject Plantet7;

    public void Update()
    {
        if (((Plantet7.transform.position.x - planet7Pedestal.transform.position.x <= 4) && (planet7Pedestal.transform.position.x - Plantet7.transform.position.x <= 4)) &&
            ((Plantet7.transform.position.y - planet7Pedestal.transform.position.y <= 4) && (planet7Pedestal.transform.position.y - Plantet7.transform.position.y <= 4)) &&
            ((Plantet7.transform.position.z - planet7Pedestal.transform.position.z <= 4) && (planet7Pedestal.transform.position.z - Plantet7.transform.position.z <= 4)) && IsActive)
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

