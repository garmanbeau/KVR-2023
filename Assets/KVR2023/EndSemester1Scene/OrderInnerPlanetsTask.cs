using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInnerPlanetsTask : TaskBase
{
    public override void StartTask()
    {
        StartSubs();
    }

    public override void StopTask()
    {
        return;
    }
}
