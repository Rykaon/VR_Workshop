using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableAttachTrigger : MonoBehaviour
{
    [SerializeField] public GrabbableObject objectToCheck;
    [SerializeField] public List<DiskCacheAttach> diskCacheAttaches;
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

                        if (!diskCache.isSnap)
                        {
                            diskCache.Snap();
                            Debug.Log("yooooooooooooooooooooo");
                        }
                    }
                }
                else if (objectToCheck == null && isDiskCache)
                {
                    DiskCache diskCache = grab as DiskCache;

                    if (diskCache != null)
                    {
                        objectToCheck = diskCache;
                        diskCache.trigger = this;
                        diskCache.diskCacheAttaches = diskCacheAttaches;
                        diskCache.body.useGravity = false;
                        diskCache.body.isKinematic = true;
                        foreach (DiskCacheAttach attach in diskCacheAttaches)
                        {
                            attach.diskCache = diskCache;
                        }
                        diskCache.Snap();
                        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
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
