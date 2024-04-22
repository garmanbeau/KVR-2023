using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Script is based on this guide: https://www.youtube.com/watch?v=pKSUhsyrj_4&t=654s
 * This script creates a umbilical cord that behaves like a chain does.
 * The above guide only served as a starting point for how to write this. Because of the child parts in each of the prefabs and the need to be connected to the ROv from the start,
 * this forced the script to be very different.
 * Do note that this script assumes that the prefabs have child objects that the hinge joints are connected to, and each child object has two hingejoints each
 * Created by: William HP.
 * Last Edited: July 5th, 2022
 */

public class RopeCreator : MonoBehaviour
{
    public enum Axis { X, Y, Z, NX, NY, NZ};
    public Axis direction;
    public bool spawn = false;
    public GameObject prefab;
    public GameObject EndPrefab;
    public int Ropelength = 5;
    public float Partdistance = 0.21f;
    public float StartDistance = 2.75f;
    public Transform StartPoint;
    ConfigurableJoint[] HingeJoint;
    private Vector3 position;
    private Vector3 angles;

    GameObject temp;//temp is place here so it can be passed between the various if statements
    ConfigurableJoint joint;
    ConfigurableJoint joint02;

    private void Start()
    {
        var temp = this.gameObject.name;
        if(temp == "Mouse")
        {
            position = new Vector3(3.745f, 0.717f, -2.825f);
            angles = new Vector3(0, 0, -90);
        }
        else if(temp == "Keyboard")
        {
            position = new Vector3(3.745f, 0.717f, -3.274f);
            angles = new Vector3(0, 0, -90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            Spawn();
            spawn = false;//This stops the script from being run endlessly
        }
    }

    private void Spawn()
    {
        int count = (int)(Ropelength / Partdistance);//This determines how many 'links' there are in the rope over a given length

        for(int i = 0; i < count; i++)
        {
            if (direction == Axis.X)//All blocks of code at this level are the same, just minor changes to where objects are place in relation to this object
            {
                if (i == 0)
                {
                    temp = Instantiate(prefab, new Vector3(StartPoint.position.x + (Partdistance * (i + StartDistance)), StartPoint.position.y, StartPoint.position.z), Quaternion.Euler(new Vector3(0, 0, 90)));
                    HingeJoint = temp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    //temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (i > 0 && i + 1 < count)//This is were the script spends most of it's time
                {
                    var localTemp = Instantiate(prefab, new Vector3(StartPoint.position.x + (Partdistance * (i + StartDistance)), StartPoint.position.y, StartPoint.position.z), Quaternion.Euler(new Vector3(0, 0, 90)));
                    localTemp.name = i.ToString();
                    HingeJoint = localTemp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = localTemp.GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    temp = localTemp;
                    
                    //localTemp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if(i + 1 == count)
                {
                    var localTemp = Instantiate(EndPrefab, new Vector3(StartPoint.position.x + (Partdistance * (i + StartDistance)), StartPoint.position.y, StartPoint.position.z), Quaternion.Euler(new Vector3(0, 0, 90)));
                    localTemp.name = i.ToString();
                    HingeJoint = localTemp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = localTemp.GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    localTemp.GetComponent<PcPartLocation>().Position = position;
                    localTemp.GetComponent<PcPartLocation>().Angles = angles;
                //    localTemp.GetComponent<PcPartLocation>().CorrectCollider = "PowerSocket";
                }
            }
            else if (direction == Axis.Y)
            {
                if (i == 0)
                {
                    temp = Instantiate(prefab, new Vector3(StartPoint.position.x, StartPoint.position.y + (Partdistance * (i + StartDistance)), StartPoint.position.z), Quaternion.Euler(new Vector3(0, 0, 180)));
                    HingeJoint = temp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    //temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (i > 0 && i + 1 < count)
                {
                    var localTemp = Instantiate(prefab, new Vector3(StartPoint.position.x, StartPoint.position.y + (Partdistance * (i + StartDistance)), StartPoint.position.z), Quaternion.Euler(new Vector3(0, 0 , 0)));
                    localTemp.name = i.ToString();
                    HingeJoint = localTemp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = localTemp.GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    temp = localTemp;
                    //localTemp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (i + 1 == count)
                {
                    var localTemp = Instantiate(EndPrefab, new Vector3(StartPoint.position.x, StartPoint.position.y + (Partdistance * (i + StartDistance)), StartPoint.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
                    localTemp.name = i.ToString();
                    HingeJoint = localTemp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = localTemp.GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    localTemp.GetComponent<PcPartLocation>().Position = position;
                    localTemp.GetComponent<PcPartLocation>().Angles = angles;
                 //   localTemp.GetComponent<PcPartLocation>().CorrectCollider = "PowerSocket";
                    //localTemp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            else if (direction == Axis.Z)
            {
                if (i == 0)
                {
                    temp = Instantiate(prefab, new Vector3(StartPoint.position.x, StartPoint.position.y, StartPoint.position.z + (Partdistance * (i + StartDistance))), Quaternion.Euler(new Vector3(-90, 0, 0)));
                    HingeJoint = temp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    //temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (i > 0 && i+ 1 < count)
                {
                    var localTemp = Instantiate(prefab, new Vector3(StartPoint.position.x, StartPoint.position.y, StartPoint.position.z + (Partdistance * (i + StartDistance))), Quaternion.Euler(new Vector3(-90, 0, 0)));
                    localTemp.name = i.ToString();
                    HingeJoint = localTemp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = localTemp.GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    temp = localTemp;
                    //localTemp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (i + 1 == count)
                {
                    var localTemp = Instantiate(EndPrefab, new Vector3(StartPoint.position.x, StartPoint.position.y, StartPoint.position.z + (Partdistance * (i + StartDistance))), Quaternion.Euler(new Vector3(-90, 0, 0)));
                    localTemp.name = i.ToString();
                    HingeJoint = localTemp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = localTemp.GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    localTemp.GetComponent<PcPartLocation>().Position = position;
                    localTemp.GetComponent<PcPartLocation>().Angles = angles;
                  //  localTemp.GetComponent<PcPartLocation>().CorrectCollider = "PowerSocket";
                    //localTemp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            else if (direction == Axis.NX)
            {
                if (i == 0)
                {
                    temp = Instantiate(prefab, new Vector3(StartPoint.position.x - (Partdistance * (i + StartDistance)), StartPoint.position.y, StartPoint.position.z), Quaternion.Euler(new Vector3(0, 0, -90)));
                    HingeJoint = temp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    //temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (i > 0 && i + 1 < count)
                {
                    var localTemp = Instantiate(prefab, new Vector3(StartPoint.position.x - (Partdistance * (i + StartDistance)), StartPoint.position.y, StartPoint.position.z), Quaternion.Euler(new Vector3(0, 0, -90)));
                    localTemp.name = i.ToString();
                    HingeJoint = localTemp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = localTemp.GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    temp = localTemp;
                    //localTemp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (i + 1 == count)
                {
                    var localTemp = Instantiate(EndPrefab, new Vector3(StartPoint.position.x - (Partdistance * (i + StartDistance)), StartPoint.position.y, StartPoint.position.z), Quaternion.Euler(new Vector3(0, 0, -90)));
                    localTemp.name = i.ToString();
                    HingeJoint = localTemp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = localTemp.GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    localTemp.GetComponent<PcPartLocation>().Position = position;
                    localTemp.GetComponent<PcPartLocation>().Angles = angles;
                   // localTemp.GetComponent<PcPartLocation>().CorrectCollider = "PowerSocket";
                    //localTemp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            else if (direction == Axis.NY)
            {
                if (i == 0)
                {
                    temp = Instantiate(prefab, new Vector3(StartPoint.position.x, StartPoint.position.y - (Partdistance * (i + StartDistance)), StartPoint.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
                    HingeJoint = temp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();

                    //temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (i > 0 && i + 1 < count)
                {
                    var localTemp = Instantiate(prefab, new Vector3(StartPoint.position.x, StartPoint.position.y - (Partdistance * (i + StartDistance)), StartPoint.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
                    localTemp.name = i.ToString();
                    HingeJoint = localTemp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = localTemp.GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    temp = localTemp;
                    //localTemp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (i + 1 == count)
                {
                    var localTemp = Instantiate(EndPrefab, new Vector3(StartPoint.position.x, StartPoint.position.y - (Partdistance * (i + StartDistance)), StartPoint.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
                    localTemp.name = i.ToString();
                    HingeJoint = localTemp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = localTemp.GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    localTemp.GetComponent<PcPartLocation>().Position = position;
                    localTemp.GetComponent<PcPartLocation>().Angles = angles;
                  //  localTemp.GetComponent<PcPartLocation>().CorrectCollider = "PowerSocket";
                    //localTemp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            else if (direction == Axis.NZ)
            {
                if (i == 0)
                {
                    temp = Instantiate(prefab, new Vector3(StartPoint.position.x, StartPoint.position.y, StartPoint.position.z - (Partdistance * (i + StartDistance))), Quaternion.Euler(new Vector3(90, 0, 0)));
                    HingeJoint = temp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                }
                else if (i > 0 && i + 1 < count)
                {
                    var localTemp = Instantiate(prefab, new Vector3(StartPoint.position.x, StartPoint.position.y, StartPoint.position.z - (Partdistance * (i + StartDistance))), Quaternion.Euler(new Vector3(90, 0, 0)));
                    localTemp.name = i.ToString();
                    HingeJoint = localTemp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = localTemp.GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    temp = localTemp;
                }
                else if (i + 1 == count)
                {
                    var localTemp = Instantiate(EndPrefab, new Vector3(StartPoint.position.x, StartPoint.position.y, StartPoint.position.z - (Partdistance * (i + StartDistance))), Quaternion.Euler(new Vector3(90, 0, 0)));
                    localTemp.name = i.ToString();
                    HingeJoint = localTemp.GetComponentsInChildren<ConfigurableJoint>();
                    joint = HingeJoint[0];
                    joint02 = HingeJoint[1];
                    joint.connectedBody = localTemp.GetComponent<Rigidbody>();
                    joint02.connectedBody = temp.GetComponent<Rigidbody>();
                    localTemp.GetComponent<PcPartLocation>().Position = position;
                    localTemp.GetComponent<PcPartLocation>().Angles = angles;
                  //  localTemp.GetComponent<PcPartLocation>().CorrectCollider = "PowerSocket";
                }
            }

        }
        #region OLD_METHOD
        /*for(int i = 0; i < count; i++)
        {
            if (i == 0)//This is to make sure that the first 'link' is properly connected, while saving the one empty joint for the next link
            {
                temp = Instantiate(StartPrefab, new Vector3(transform.position.x, transform.position.y + (Partdistance * (i + 2.75f)), transform.position.z), Quaternion.identity);
                //The 2.75f works best for the ROV in this scene, so it may be best to rewrite as a seperate private float to make it easier to adapt the script to other objects

                TopSphereStart = temp.transform.GetChild(0).gameObject;
                BottomSphereStart = temp.transform.GetChild(1).gameObject;
                HingeJointStart01 = TopSphereStart.GetComponentsInChildren<HingeJoint>();
                HingeJointStart02 = BottomSphereStart.GetComponentsInChildren<HingeJoint>();
                joint = HingeJointStart02[0];
                joint02 = HingeJointStart02[1];
                joint03 = HingeJointStart01[0];
                joint04 = HingeJointStart01[1];
                joint.connectedBody = GetComponent<Rigidbody>();
                joint02.connectedBody = temp.GetComponent<Rigidbody>();
                joint03.connectedBody = temp.GetComponent<Rigidbody>();
            }
            else if (i == 1)//Because the second 'link' needs to be connected to the start of the rope, this is here to make sure that the start is properly connected
            {
                temp = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y + (Partdistance * (i + 2.75f)), transform.position.z), Quaternion.identity);
                temp.name = i.ToString();
                joint04.connectedBody = temp.GetComponent<Rigidbody>();
                HingeJoint = temp.GetComponentsInChildren<HingeJoint>();
                joint = HingeJoint[0];
                joint02 = HingeJoint[1];
                joint.connectedBody = temp.GetComponent<Rigidbody>();
            }
            else if (i > 1 && i + 1 != count)//This is were the script spends most of it's time
            {
                temp = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y + (Partdistance * (i + 2.75f)), transform.position.z), Quaternion.identity);
                temp.name = i.ToString();
                joint02.connectedBody = temp.GetComponent<Rigidbody>();
                HingeJoint = temp.GetComponentsInChildren<HingeJoint>();
                joint = HingeJoint[0];
                joint.connectedBody = temp.GetComponent<Rigidbody>();
                joint02 = HingeJoint[1];
            }
            else if (i + 1 == count)//End the rope, and also prevent it from leaving it's spot so it behaves like an umbilical cord. Could put an if statement here to switch between different behavivors
            {
                Rigidbody rb;
                temp = Instantiate(EndPrefab, new Vector3(transform.position.x, transform.position.y + (Partdistance * (i + 2.75f)), transform.position.z), Quaternion.identity);
                rb = temp.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePosition;
                temp.name = i.ToString();
                joint02.connectedBody = temp.GetComponent<Rigidbody>();
            }
        }*/
        #endregion
    }
}
