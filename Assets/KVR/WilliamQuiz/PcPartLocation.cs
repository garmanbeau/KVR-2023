using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Grabbable))]

public class PcPartLocation : MonoBehaviour
{
    public AudioClip FoundRight;
    public AudioClip FoundWrong;
    //public AudioClip FoundAgain;
    public AudioClip Done;
    public AudioSource play;
    public bool GrabThis = false;
   
    public bool inPlace = false;
    public bool NextQuestionUp = false;
   // public bool Paired = false;
   // [Tooltip("Only 1 in the pair can have this enabled")]public bool PairCaller = false;
    [Tooltip("How fast the object moves to the given location")]public float speed = 0.01f;
 //  public GameObject FullScaleObj;
  //  [Tooltip("Only put something here when you have Paired set to true")]public GameObject PairedObject;
    public HPQuizController NextQuestion;
    
    private Vector3 StartPosition;

    // mg
    internal Vector3 Position = new Vector3();    
    internal Vector3 Angles = new Vector3();     
    private Grabbable thisGrabbable;
    private bool isGrabbed = false;
    private bool played = false;
    Rigidbody rigidbody;
    public List<PcPartLocation> otherPcParts;
    //mg
    public GameObject targetObject;
    public float returnHomeTime = 2.0f;
    public UnityEvent onGrabbed;
    public UnityEvent onReleased;
    public UnityEvent partPlaced;
    public UnityEvent onSetComplete;
    [HideInInspector]public bool QuestionSet = false;

    // Start is called before the first frame update
    void Start()
    {
        thisGrabbable = GetComponent<Grabbable>();
        Position = targetObject.transform.position;
        Angles = targetObject.transform.rotation.eulerAngles;
      //  VRLineRenderer = GetComponent<VRLineRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        Debug.Log("Location of Position for " + gameObject.name + " is: ("+Position.x+", "+Position.y+", "+Position.z+")");
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(RightSpot == true && Vector3.Distance(transform.localPosition, Position) > 0.1f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Position, speed);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Angles), speed);
            speed += 0.01f;
        }
        else if(RightSpot == true && Vector3.Distance(Position, transform.localPosition) <= 0.1f)
        {
            transform.localPosition = Position;
            transform.localRotation = Quaternion.Euler(Angles);
            InPlace = true;
            GrabThis = false;
        }
       
        if(InPlace == true && QuestionSet == false && Paired == false)
        {
            play.PlayOneShot(Done);
            //NextQuestion.PresentQuestion();
            QuestionSet = true;
            GetComponent<VRLineRenderer>().enabled = false;
            FullScaleObj.SetActive(true);
            transform.gameObject.SetActive(false);
        }
        else if (InPlace == true && QuestionSet == false && Paired == true && PairCaller == false)
        {
            GetComponent<VRLineRenderer>().enabled = false;
            FullScaleObj.SetActive(true);
            transform.gameObject.SetActive(false);
        }
        else if(InPlace == true && QuestionSet == false && Paired == true && PairCaller == true)
        {
            if(PairedObject.GetComponent<PcPartLocation>().InPlace == true)
            {
                //NextQuestion.PresentQuestion();
                play.PlayOneShot(Done);
                QuestionSet = true;
                GetComponent<VRLineRenderer>().enabled = false;
                FullScaleObj.SetActive(true);
                transform.gameObject.SetActive(false);
            }
        }

         */
    }

    public void CheckGrab()
    {
       /* if (isGrabbed)
        {
            Debug.Log("Found Correct Part");
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Particles"))
            {
                Debug.Log("Called forEach obj");
                obj.SetActive(false); 
            }
        }*/
        if (!isGrabbed)
        {
            Debug.Log("GrabThis && !isGrabbed");
            if (play.isPlaying)
            {
                play.Stop();
            }
            if (!played)
            { play.PlayOneShot(FoundRight); played = true; }
            onGrabbed.Invoke();
            isGrabbed = true;
            Debug.Log("Found Correct Part");
            var childcount = transform.childCount;
           /* foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Particles"))
            {
                Debug.Log("Called forEach obj");
                if (obj != transform.GetChild(childcount -1))
                { obj.SetActive(false); }
            }*/
          //  VRLineRenderer.StartDrawing();
        }
        else if(!GrabThis & !isGrabbed)
        {
            var temp = GameObject.FindGameObjectsWithTag("PCpart");
            foreach(GameObject part in temp)
            {
                if(part.GetComponent<PcPartLocation>().GrabThis == true)
                {
                    if (play.isPlaying)
                    {
                        play.Stop();
                    }
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
        onReleased.Invoke();
        //StartCoroutine(ResetLocation());
      
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
        while (timer<=returnHomeTime)
        {
            transform.position = Vector3.Lerp(transform.position, Position, timer / returnHomeTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Angles), timer / returnHomeTime);
            timer += Time.deltaTime;
            yield return null;
        }
        bool setComplete = false;
        partPlaced.Invoke();
        if (otherPcParts.Count> 0)
        {
            foreach (var part in otherPcParts)
            {
                if (part == this) continue;
                if (!part.inPlace)
                {
                    setComplete = false;
                    break;
                }
                else
                { setComplete = true; }
            }
        }
        else
        { setComplete = true; }

        if  (setComplete)
        {
            if (play.isPlaying)
            {
                play.Stop();
            }
            play.PlayOneShot(Done);
           
            QuestionSet = true;

            onSetComplete.Invoke();
        }

        
        /*
        if (!NextQuestionUp && !Paired)
        {

            if (play.isPlaying)
            {
                play.Stop();
            }
            play.PlayOneShot(Done);
            //NextQuestion.PresentQuestion();
            QuestionSet = true;
     
            transform.gameObject.SetActive(false);
        }
        else if (!NextQuestionUp && Paired && PairCaller )
        {
            if (PairedObject.GetComponent<PcPartLocation>().inPlace )
            {
                NextQuestion.PresentQuestion();
                NextQuestionUp = true;
            
            }
        }
        else if (inPlace && !QuestionSet && Paired && PairCaller )
        {
            if (PairedObject.GetComponent<PcPartLocation>().inPlace)
            {
                if (play.isPlaying)
                {
                    play.Stop();
                }
                play.PlayOneShot(Done);
                QuestionSet = true;
           
                transform.gameObject.SetActive(false);
            }
        }
        */

    }

    public void Question()
    {
        if (inPlace && !NextQuestionUp && !play.isPlaying)
        { NextQuestion.PresentQuestion(); NextQuestionUp = true; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "GrabbableCatcher")
        { transform.position = StartPosition; }
        /*
        if(collision.gameObject.name == targetObject.name)
        {
            var Mtemp = GetComponent<Collider>();
            var rb = GetComponent<Rigidbody>();
            thisGrabbable.enabled = false;
            rb.useGravity = false;
            Mtemp.enabled = false;
            RightSpot = true;
        }*/
    }
}
