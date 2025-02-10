using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjectScript : MonoBehaviour
{
    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGrab()
    {
        rb.isKinematic = false;
        Debug.Log(rb.isKinematic);
    }

    public void EndGrab()
    {
        rb.isKinematic = false;
    }

    public void StartGrav()
    {
        Debug.Log("CAPSULE");
    }
}

