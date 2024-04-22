using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScrpt : MonoBehaviour
{
    [SerializeField]
    public float rotationSpeed  = 100.0f;
    float timer = 0.0f;
    float delay = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log (rotationSpeed.ToString ());  
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer> delay)
        {
            timer = 0;
            rotationSpeed += 200.0f;
            Debug.Log(rotationSpeed.ToString());
        }
    }
}
