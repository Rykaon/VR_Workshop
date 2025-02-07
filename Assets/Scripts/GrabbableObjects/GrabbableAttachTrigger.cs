using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableAttachTrigger : MonoBehaviour
{
    [SerializeField] public GrabbableObject objectToCheck;
    [SerializeField] public List<DiskCacheAttach> diskCacheAttaches;
    [SerializeField] public GrabbableAttachedObject diskToAttach;
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
                        }
                    }
                }
                else if (objectToCheck == null && isDiskCache)
                {
                    Debug.Log("PIPI");
                    DiskCache diskCache = grab as DiskCache;

                    if (diskCache != null)
                    {
                        objectToCheck = diskCache;
                        diskCache.trigger = this;
                        diskCache.diskCacheAttaches = diskCacheAttaches;
                        diskCache.disk = diskToAttach;

                        foreach(DiskCacheAttach attach in diskCache.diskCacheAttaches)
                        {
                            attach.diskCache = diskCache;
                        }

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
                    Debug.Log(other.gameObject);
                    isObjectInBox = false;
                }
            }
        }
    }

    public void Update()
    {
        GrabbableAttachedObject obj = objectToCheck as GrabbableAttachedObject;

        if (obj != null)
        {
            if (obj.isDoor)
            {
                Debug.Log(isObjectInBox);
            }
        }
    }
}
