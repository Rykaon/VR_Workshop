using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerGrab : PlayerComponent
{
    bool isGrabbing = false;
    GrabbableObject grabObject;

    public override void InitializeComponent(PlayerController controller)
    {
        base.InitializeComponent(controller);
        controller.leftInteractor.interactionLayers = InteractionLayerMask.GetMask("GrabObjects");
        controller.rightInteractor.interactionLayers = InteractionLayerMask.GetMask("GrabObjects");
    }

    private void SetGrip(HandTrigger hand, bool value)
    {
        if (hand == null)
        {
            return;
        }

        XRDirectInteractor interactor;

        if (hand == controller.leftTrigger)
        {
            interactor = controller.leftInteractor;
        }
        else
        {
            interactor = controller.rightInteractor;
        }

        if (isGrabbing != value)
        {
            isGrabbing = value;

            if (value)
            {
                GrabbableObject grab = hand.GetClosestGrabObject();

                if (grab != null)
                {
                    grabObject = grab;
                    hand.AttachGrabbableObject(grabObject, interactor);
                }
            }
            else
            {
                if (grabObject != null)
                {
                    hand.DettachGrabbableObject(grabObject);
                    grabObject = null;
                }
            }
        }
    }

    public override void UpdateComponent()
    {
        float leftGripValue = controller.inputAction.FindActionMap("XRI LeftHand interaction").FindAction("Select Value").ReadValue<float>();
        float rightGripValue = controller.inputAction.FindActionMap("XRI RightHand interaction").FindAction("Select Value").ReadValue<float>();

        if (leftGripValue > 0)
        {
            SetGrip(controller.leftTrigger, true);
        }
        else
        {
            SetGrip(controller.leftTrigger, false);
        }

        if (rightGripValue > 0)
        {
            SetGrip(controller.rightTrigger, true);
        }
        else
        {
            SetGrip(controller.rightTrigger, false);
        }
    }

    public void Update()
    {
        UpdateComponent();
    }
}
