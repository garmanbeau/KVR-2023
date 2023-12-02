using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlanet2 : TaskBase
{
    /// <summary>
    /// This class is the implementation select the largest planet in the KVR2023 Sprint6Scene. 
    /// When the user grabs Jupiter in the scene, an event is triggered. 
    /// </summary>

    [SerializeField] private VisualAffordance VisualAffordance;
    [SerializeField] private GameObject planet2Pedestal;
    [SerializeField] private GameObject Planet2;
    public void Start()
    {

    }

    public void Update()
    {
        if (((Planet2.transform.position.x - planet2Pedestal.transform.position.x <= 4) && (planet2Pedestal.transform.position.x - Planet2.transform.position.x <= 4)) &&
            ((Planet2.transform.position.y - planet2Pedestal.transform.position.y <= 4) && (planet2Pedestal.transform.position.y - Planet2.transform.position.y <= 4)) &&
            ((Planet2.transform.position.z - planet2Pedestal.transform.position.z <= 4) && (planet2Pedestal.transform.position.z - Planet2.transform.position.z <= 4)) && IsActive)
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

