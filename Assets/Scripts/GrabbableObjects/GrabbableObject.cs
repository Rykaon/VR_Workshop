using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableObject : MonoBehaviour
{
    public bool isGrab = false;
    [SerializeField] XRGrabInteractable grabInteractible;
    [SerializeField] Rigidbody rigidBody;

    void Start()
    {
        
    }

    public virtual void SetIsGrab(bool value)
    {
        if (isGrab != value)
        {
            isGrab = value;

            if (value)
            {

            }
            else
            {

            }
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (isGrab)
        {
            
        }
    }
}
