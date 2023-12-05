using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnablePageText : MonoBehaviour
{
    public UnityEvent startTextUpdate;

    private void OnEnable() //Called via Unity Magic everytime the GameObject this class/script is attached to is set active.
    {
        startTextUpdate?.Invoke();
    }
}
