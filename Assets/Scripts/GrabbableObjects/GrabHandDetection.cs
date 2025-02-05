using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabHandDetection : MonoBehaviour
{
    public GrabbableObject grabbableObject;
    public XRGrabInteractable grabInteractable;
    public PlayerGrab grab;
    public HandTrigger trigger;
    private string activeHand = ""; // "LeftHand" ou "RightHand"

    public void OnHoverEnter(SelectEnterEventArgs args)
    {
        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
        grab = interactor.transform.parent.GetComponentInParent<PlayerGrab>();

        if (grab == null)
        {
            return;
        }

        if (interactor == grab.controller.leftInteractor)
        {
            activeHand = "LeftHand";
            trigger = grab.controller.leftTrigger;
        }
        else
        {
            activeHand = "RightHand";
            trigger = grab.controller.rightTrigger;
        }

        if (!trigger.grabbableObjects.Contains(grabbableObject))
        {
            trigger.grabbableObjects.Add(grabbableObject);
        }
    }

    public void OnHoverExit(SelectExitEventArgs args)
    {
        if (trigger.grabbableObjects.Contains(grabbableObject))
        {
            trigger.grabbableObjects.Remove(grabbableObject);
        }
    }
}
