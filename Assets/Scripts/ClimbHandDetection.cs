using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbHandDetection : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public PlayerMovement playerMovement;
    private string activeHand = ""; // "LeftHand" ou "RightHand"

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

   

    public void OnGrab(SelectEnterEventArgs args)
    {
        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
        Debug.Log(interactor.tag);
        if (interactor.CompareTag("LeftHand"))
        {
            activeHand = "LeftHand";
        }
        else if (interactor.CompareTag("RightHand"))
        {
            activeHand = "RightHand";
        }

        playerMovement = interactor.GetComponentInParent<PlayerMovement>(); // Récupérer PlayerMovement
        if (playerMovement != null)
        {
            playerMovement.StartClimb(activeHand); // Passer la main qui grimpe
        }
    }

    public void OnRelease(SelectExitEventArgs args)
    {
        if (playerMovement != null)
        {
            playerMovement.StopClimbing();
        }
    }
}