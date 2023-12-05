using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehavior : MonoBehaviour
{
    GameObject childGameObject;
    Rigidbody rb;

    private void Start()
    {
        childGameObject = transform.GetChild(0).gameObject;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ToggleGravity(true);          
    }

    public void TogglePlanetText(bool hasText)
    {
        childGameObject.SetActive(hasText);
    }

    public void ToggleGravity(bool usesGravity)
    {
        rb.useGravity = usesGravity;
    }
}
