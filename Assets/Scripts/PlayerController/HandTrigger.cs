using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
