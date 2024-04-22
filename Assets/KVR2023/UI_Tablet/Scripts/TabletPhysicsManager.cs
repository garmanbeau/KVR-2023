using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletPhysicsManager : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Enable continuous collision detection
    }

    public void UnlockObject()
    {
        rb.isKinematic = false;
    }

    public void LockObject()
    {
        rb.isKinematic = true;
    }
}
