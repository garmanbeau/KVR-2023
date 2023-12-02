using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using UnityEngine.Events;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Grabbable))]

public class CablePartLocation : MonoBehaviour
{
    public AudioClip FoundRight;
    public AudioClip FoundWrong;
    public AudioClip Done;
    public AudioSource play;
    public bool GrabThis = false;
    public bool RightSpot = false;
    public bool InPlace = false;
    public float returnHomeTime = 2.0f;
    public GameObject targetObject;
    public GameObject[] CableParts;
    public UnityEvent partPlaced;

    internal Vector3 Position = new Vector3();
    internal Vector3 Angles = new Vector3();
    private Grabbable thisGrabbable;
    private bool isGrabbed = false;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        thisGrabbable = GetComponent<Grabbable>();
        Position = targetObject.transform.position;
        Angles = targetObject.transform.rotation.eulerAngles;
        rigidbody = GetComponent<Rigidbody>();
    }

    public void CheckGrab()
    {
        if (GrabThis && !isGrabbed && !play.isPlaying)
        {
            play.PlayOneShot(FoundRight);
            isGrabbed = true;
            Debug.Log("Found Correct Part");
            //  VRLineRenderer.StartDrawing();
        }
        else if (!GrabThis & !isGrabbed && !play.isPlaying)
        {
            var temp = GameObject.FindGameObjectsWithTag("PCpart");
            foreach (GameObject part in temp)
            {
                if (part.GetComponent<PcPartLocation>().GrabThis == true)
                {
                    var audio = part.GetComponent<PcPartLocation>().FoundWrong;
                    play.PlayOneShot(audio);
                    break;
                }
            }
            isGrabbed = true;
        }
    }

    public void UnGrab()
    {
        isGrabbed = false;
        // VRLineRenderer.StopDrawing();

    }

    private void NoPhysics()
    { 
        foreach(GameObject obj in CableParts)
        {
            for(int i = 0; i < obj.transform.childCount; i++)
            {
                Transform temp = obj.transform.GetChild(i);
                if (temp.GetComponent<GameObject>() != null)
                {
                    GameObject bit = temp.GetComponent<GameObject>();
                    bit.GetComponent<CapsuleCollider>().enabled = false;
                    bit.GetComponent<Rigidbody>().isKinematic = true;
                }
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
            //  thisGrabbable.enabled = false;
            rigidbody.useGravity = false;

            RightSpot = true;
            NoPhysics();
            StartCoroutine(PlacePart());
        }
    }

    public void UpdatePlace()
    { InPlace = true; }

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
        transform.position = Position;
        transform.localRotation = Quaternion.Euler(Angles);
        rigidbody.constraints = RigidbodyConstraints.FreezePosition;

        partPlaced.Invoke();
    }
}