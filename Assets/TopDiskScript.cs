using Meta.WitAi;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class TopDiskScript : MonoBehaviour
{
    [SerializeField] private GameObject[] allScrews;
    [SerializeField] private FixedJoint joint;
     public GameManager GM;

    private void OpenTopPart()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        transform.SetParent(null);
        body.isKinematic = false;
        body.useGravity = false;
        GetComponent<XRGrabInteractable>().enabled = true;
        Destroy(joint);
        GM.firstDisk.welded = false;
    }

    public void CheckIfOpen()
    {
        bool result = false;
        foreach (GameObject go in allScrews)
        {
            if (go.GetComponent<Renderer>().enabled == true)
            {
                result = true;
            }
        }
        if (!result)
        {
            OpenTopPart();
        }
    }
}
