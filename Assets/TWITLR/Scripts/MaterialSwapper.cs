using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMaterialSwaper : MonoBehaviour
{

    public Material Material1;

    public GameObject[] Corners; 

    //in the editor this is what you would set as the object you wan't to change
    public void ChangeMaterial()
    {
        Debug.Log("ChangeMaterial called");

        foreach (GameObject j in Corners)
        {
            j.GetComponent<Renderer>().material = Material1;
        }
    }
}