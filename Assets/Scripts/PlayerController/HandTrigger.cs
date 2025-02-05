using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandTrigger : MonoBehaviour
{
    List<GrabbableObject> grabbableObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GrabbableObject>(out GrabbableObject grab))
        {
            if (!grabbableObjects.Contains(grab))
            {
                grabbableObjects.Add(grab);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<GrabbableObject>(out GrabbableObject grab))
        {
            if (grabbableObjects.Contains(grab))
            {
                grabbableObjects.Remove(grab);
            }
        }
    }

    public GrabbableObject GetClosestGrabObject()
    {
        if (grabbableObjects.Count == 0)
        {
            return null;
        }

        float minDistance = float.MaxValue;
        int index = -1;

        for (int i = 0; i < grabbableObjects.Count; i++)
        {
            float distance = Mathf.Abs(Vector3.Distance(grabbableObjects[i].transform.position, transform.position));
            
            if (distance < minDistance)
            {
                minDistance = distance;
                index = i;
            }
        }

        if (index == -1)
        {
            return null;
        }
        else 
        {
            return grabbableObjects[index];
        }
    }

    public void AttachGrabbableObject(GrabbableObject grab, XRDirectInteractor interactor)
    {
        grab.SetIsGrab(true, interactor);
    }

    public void DettachGrabbableObject(GrabbableObject grab)
    {
        grab.SetIsGrab(false, null);
    }
}
