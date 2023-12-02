using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlanet8 : TaskBase
{
    /// <summary>
    /// This class is the implementation select the largest planet in the KVR2023 Sprint6Scene. 
    /// When the user grabs Jupiter in the scene, an event is triggered. 
    /// </summary>

    [SerializeField] private VisualAffordance VisualAffordance;
    [SerializeField] private GameObject planet8Pedestal;
    [SerializeField] private GameObject Planet8;

    public void Update()
    {
        if (((Planet8.transform.position.x - planet8Pedestal.transform.position.x <= 1) && (planet8Pedestal.transform.position.x - Planet8.transform.position.x <= 1)) &&
            ((Planet8.transform.position.y - planet8Pedestal.transform.position.y <= 1) && (planet8Pedestal.transform.position.y - Planet8.transform.position.y <= 1)) &&
            ((Planet8.transform.position.z - planet8Pedestal.transform.position.z <= 1) && (planet8Pedestal.transform.position.z - Planet8.transform.position.z <= 1)) && IsActive)
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

