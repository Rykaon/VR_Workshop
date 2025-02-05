using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableObject : MonoBehaviour
{
    public bool canBeGrab = false;
    public bool isGrab = false;
    [SerializeField] XRGrabInteractable grabInteractable;
    [SerializeField] Rigidbody rigidBody;

    void Start()
    {
        InitializeObject();
    }

    protected virtual void InitializeObject()
    {
        //grabInteractible.enabled = canBeGrab;

        SetCanBeGrab(canBeGrab);
    }

    protected virtual void SetCanBeGrab(bool canBeGrab)
    {
        this.canBeGrab = canBeGrab;

        if (canBeGrab)
        {
            grabInteractable.interactionLayers = InteractionLayerMask.GetMask("GrabObjects");
        }
        else
        {
            grabInteractable.interactionLayers = InteractionLayerMask.GetMask("IgnoreInteractions");
        }
    }

    public virtual void SetIsGrab(bool value)
    {
        if (isGrab != value)
        {
            isGrab = value;

            if (value)
            {
                SetCanBeGrab(true);
            }
            else
            {
                SetCanBeGrab(false);
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
