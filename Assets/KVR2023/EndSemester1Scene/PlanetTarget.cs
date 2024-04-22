using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlanetTarget : MonoBehaviour
{
    [SerializeField] private TaskBase task;
    private string correctPlanet;

    private void Start()
    {
        correctPlanet = gameObject.tag;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (task.IsActive)
        {
            GameObject planet = other.gameObject;
            string planetName = planet.tag;
            if(planetName == correctPlanet)
            {
                task.CompleteSub();
            }    
        }
    }
}
