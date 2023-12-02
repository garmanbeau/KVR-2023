using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlanet4 : TaskBase
{
    /// <summary>
    /// This class is the implementation select the largest planet in the KVR2023 Sprint6Scene. 
    /// When the user grabs Jupiter in the scene, an event is triggered. 
    /// </summary>

    [SerializeField] private VisualAffordance VisualAffordance;
    [SerializeField] private GameObject planet4Pedestal;
    [SerializeField] private GameObject Planet4;

    public void Update()
    {
        if (((Planet4.transform.position.x - planet4Pedestal.transform.position.x <= 4) && (planet4Pedestal.transform.position.x - Planet4.transform.position.x <= 4)) &&
            ((Planet4.transform.position.y - planet4Pedestal.transform.position.y <= 4) && (planet4Pedestal.transform.position.y - Planet4.transform.position.y <= 4)) &&
            ((Planet4.transform.position.z - planet4Pedestal.transform.position.z <= 4) && (planet4Pedestal.transform.position.z - Planet4.transform.position.z <= 4)) && IsActive)
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

