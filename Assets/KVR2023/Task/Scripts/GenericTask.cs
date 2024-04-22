using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTask : TaskBase
{
    public override void StartTask()
    {
        IsActive = true;
        TextUpdateDirections();
    }

    public override void StopTask()
    {
        return;
    }
}
