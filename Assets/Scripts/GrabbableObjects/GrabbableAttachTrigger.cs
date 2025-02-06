using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableAttachTrigger : MonoBehaviour
{
    [SerializeField] GrabbableObject objectToCheck;
    public bool isObjectInBox;
    public bool isDiskCache = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GrabbableObject grab))
        {
            if (grab != null)
            {
                if (grab == objectToCheck)
                {
                    isObjectInBox = true;

                    if (isDiskCache && !grab.isGrab)
                    {
                        DiskCache diskCache = grab as DiskCache;
                        diskCache.Snap();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GrabbableObject grab))
        {
            if (grab != null)
            {
                if (grab == objectToCheck)
                {
                    isObjectInBox = false;
                }
            }
        }
    }
}
