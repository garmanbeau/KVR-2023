using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableChecker : MonoBehaviour
{
    public AudioClip QuizEnd;
    public AudioSource play;
    public GameObject MouseCable;
    public GameObject KeyboardCable;
    public GameObject MonitorVideoCable;
    //public GameObject MonitorPowerCable;
    private CablePartLocation bol1;
    private CablePartLocation bol2;
    private CablePartLocation bol3;
    //private CablePartLocation bol4;
    // Start is called before the first frame update
    void Start()
    {
        bol1 = MouseCable.GetComponent<CablePartLocation>();
        bol2 = KeyboardCable.GetComponent<CablePartLocation>();
        bol3 = MonitorVideoCable.GetComponent<CablePartLocation>();
        //bol4 = MonitorPowerCable.GetComponent<CablePartLocation>();
    }
    
    public void EndQuiz()
    {
        if (bol1.InPlace && bol2.InPlace && bol3.InPlace /*&& bol4.InPlace*/)
        { play.PlayOneShot(QuizEnd); }
    }
}
