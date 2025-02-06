using Meta.WitAi.TTS.Integrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowTorchTrigger : MonoBehaviour
{
    [SerializeField] GrabbableTool blowTorch;
    public DiskCacheAttach diskCacheAttach;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DiskCacheAttach attach))
        {
            diskCacheAttach = attach;
            Debug.Log("jhfjhgchugc");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out DiskCacheAttach attach))
        {
            if (attach == diskCacheAttach)
            {
                diskCacheAttach = null;
            }
        }
    }
}
