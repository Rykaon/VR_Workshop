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
    protected XRDirectInteractor interactor;

    [SerializeField] List<Material> grabbable;
    [SerializeField] List<Material> ungrabbable;
    MeshRenderer renderer;

    void Start()
    {
        InitializeObject();
    }

    protected virtual void InitializeObject()
    {
        //grabInteractible.enabled = canBeGrab;

        renderer = GetComponent<MeshRenderer>();
        SetCanBeGrab(canBeGrab);
    }

    protected virtual void SetCanBeGrab(bool canBeGrab)
    {
        this.canBeGrab = canBeGrab;

        if (canBeGrab)
        {
            grabInteractable.interactionLayers = InteractionLayerMask.GetMask("GrabObjects");
            renderer.SetMaterials(grabbable);
            Debug.Log("SetCanGrab to True // " + isGrab);

            if (isGrab)
            {
                SelectEnterEventArgs enterEventArgs = new SelectEnterEventArgs();
                enterEventArgs.interactorObject = interactor;

                grabInteractable.selectEntered.Invoke(enterEventArgs);
                Debug.Log("OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
            }
        }
        else
        {
            grabInteractable.interactionLayers = InteractionLayerMask.GetMask("IgnoreInteractions");
            renderer.SetMaterials(ungrabbable);
            Debug.Log("SetCanGrab to False");
        }
    }

    public virtual void SetIsGrab(bool value, XRDirectInteractor interactor)
    {
        if (isGrab != value)
        {
            isGrab = value;

            if (value)
            {
                this.interactor = interactor;
                //SetCanBeGrab(true);
            }
            else
            {
                this.interactor = null;
                //SetCanBeGrab(false);
            }
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (isGrab)
        {
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetCanBeGrab(!canBeGrab);
        }
    }
}
