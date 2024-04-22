using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureActivator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Loop through all child game objects and set them active
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
