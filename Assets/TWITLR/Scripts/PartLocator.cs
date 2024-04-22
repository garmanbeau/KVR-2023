using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Grabbable))]

public class PartLocator : MonoBehaviour
{
  
    public AudioClip Done;
    public AudioSource audioSource;


    public bool inPlace = false;


    private Vector3 StartPosition;

    // mg
    internal Vector3 Position = new Vector3();
    internal Vector3 Angles = new Vector3();
    private Grabbable thisGrabbable;
    private bool isGrabbed = false;
    private bool played = false;
    Rigidbody rigidbody;
    
    //mg
    public GameObject targetObject;
    public float returnHomeTime = 2.0f;
    public UnityEvent onGrabbed;
    public UnityEvent onReleased;
    public UnityEvent partPlaced;
    int grabPoints = 0;


    // Start is called before the first frame update
    void Start()
    {
        thisGrabbable = GetComponent<Grabbable>();
        Position = targetObject.transform.position;
        Angles = targetObject.transform.rotation.eulerAngles;
    
        rigidbody = GetComponent<Rigidbody>();
        Debug.Log("Location of Position for " + gameObject.name + " is: (" + Position.x + ", " + Position.y + ", " + Position.z + ")");
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void CheckGrab()
    {
        grabPoints++;

        StopCoroutine(ResetLocation());
        if (!isGrabbed)
        {
         
          
            onGrabbed.Invoke();
            isGrabbed = true;

        
        }

    }

    public void UnGrab()
    {

        grabPoints--;
        if (grabPoints == 0)
        {
            isGrabbed = false;
            onReleased.Invoke();
            if (!inPlace)
            {
                StartCoroutine(ResetLocation());
            }
        }

    }

    // added by MG
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == targetObject.name)
        {
            Debug.Log("Correct Part Placed");

            thisGrabbable.ForceHandsRelease();

            rigidbody.useGravity = false;

            inPlace = true;
            StartCoroutine(PlacePart());
        }
    }

    IEnumerator ResetLocation()
    {
        yield return new WaitForSeconds(2.5f);
        if (!inPlace)
        { transform.position = StartPosition; }
    }

    // added by MG
    IEnumerator PlacePart()
    {
        float timer = 0;
        while (timer <= returnHomeTime)
        {
            transform.position = Vector3.Lerp(transform.position, Position, timer / returnHomeTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Angles), timer / returnHomeTime);
            timer += Time.deltaTime;
            yield return null;
        }
       
        partPlaced.Invoke();
  


    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "GrabbableCatcher")
        { transform.position = StartPosition; }
 
    }
}
