using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiskCacheAttach : MonoBehaviour
{
    [SerializeField] public bool isActive;
    [SerializeField] DiskCache diskCache;
    [SerializeField] List<Material> active;
    [SerializeField] List<Material> notActive;
    [SerializeField] MeshRenderer meshRenderer;
    bool canBeActivated = true;

    private void Start()
    {
        //SetActive(true);
    }

    public void SetActive(bool isActive)
    {
        if (canBeActivated)
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
        diskCache.Attach();
        Debug.Log("caca");

        if (isActive)
        {
            meshRenderer.SetMaterials(active);
        }
        else
        {
            meshRenderer.SetMaterials(notActive);
        }

        canBeActivated = true;
    }
}
