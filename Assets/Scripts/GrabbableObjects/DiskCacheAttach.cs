using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiskCacheAttach : MonoBehaviour
{
    [SerializeField] public bool isActive;
    [SerializeField] public DiskCache diskCache;
    [SerializeField] MeshRenderer meshRenderer;
    bool canBeActivated = true;

    private void Start()
    {

    }

    public void SetActive(bool isActive)
    {
        if (canBeActivated && diskCache != null)
        {
            canBeActivated = false;
            Debug.Log("activate");
            StartCoroutine(Activate(isActive));
        }
    }

    private IEnumerator Activate(bool isActive)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        this.isActive = isActive;
        meshRenderer.enabled = isActive;
        diskCache.Attach();
        Debug.Log(isActive);
        if (isActive)
        {

        }
        else
        {

        }

        canBeActivated = true;
    }
}
