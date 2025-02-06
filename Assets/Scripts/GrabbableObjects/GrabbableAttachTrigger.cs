using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableAttachTrigger : MonoBehaviour
{
    [SerializeField] GrabbableAttachedObject objectToCheck;
    public bool isObjectInBox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GrabbableAttachedObject grab))
        {
            if (grab != null)
            {
                if (grab == objectToCheck)
                {
                    isObjectInBox = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GrabbableAttachedObject grab))
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
