using Meta.WitAi;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class TopDiskScript : MonoBehaviour
{
    [SerializeField] private GameObject[] allScrews;
    [SerializeField] private FixedJoint joint;

    private void OpenTopPart()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        transform.SetParent(null);
        body.isKinematic = false;
        body.useGravity = false;
        GetComponent<XRGrabInteractable>().enabled = true;
        Destroy(joint);

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
