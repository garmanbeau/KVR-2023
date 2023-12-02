using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderOuterPlanetsTask : TaskBase
{
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            TaskBase task = child.GetComponent<TaskBase>();
            subs.Insert(0, task);
        }
    }
    public override void StartTask()
    {
        this.StartSubs();
    }

    public override void StopTask()
    {
        return;
    }
}
