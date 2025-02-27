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
        if (diskCache != null)
        {
            if (canBeActivated)
            {
                canBeActivated = false;
                Debug.Log("activate");
                StartCoroutine(Activate(isActive));
            }
        }
    }

    private IEnumerator Activate(bool isActive)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        this.isActive = isActive;
        diskCache.Attach();
        Debug.Log("caca = " + isActive);

        if (isActive)
        {

        }
        else
        {

        }

        canBeActivated = true;
    }
}
