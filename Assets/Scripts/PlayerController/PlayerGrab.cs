using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerGrab : PlayerComponent
{
    bool isGrabbing = false;
    GrabbableObject leftGrabObject;
    GrabbableObject righttGrabObject;
    [SerializeField] float angleAccpetance;
    [SerializeField] float distanceAccpetance;
    [SerializeField] float velocityTreshold;
    [SerializeField] public MeshRenderer leftRenderer;
    [SerializeField] public MeshRenderer rightRenderer;

    bool isLeftHandMoving;
    bool isRightHandMoving;
    float leftHandDistance;
    float rightHandDistance;
    Vector3 previousLeftHandPosition;
    Vector3 previousRightHandPosition;

    public override void InitializeComponent(PlayerController controller)
    {
        base.InitializeComponent(controller);
        //controller.leftInteractor.interactionLayers = InteractionLayerMask.GetMask("GrabObjects");
        //controller.rightInteractor.interactionLayers = InteractionLayerMask.GetMask("GrabObjects");
    }

    private void SetGrip(HandTrigger hand, bool value)
    {
        if (hand == null)
        {
            return;
        }
        else if (value && leftGrabObject != null)
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
                    if (hand == controller.leftTrigger)
                    {
                        leftGrabObject = grab;
                    }
                    else
                    {
                        righttGrabObject = grab;
                    }
                    
                    hand.AttachGrabbableObject(grab, interactor);
                }
            }
            else
            {
                if (hand == controller.leftTrigger)
                {
                    if (leftGrabObject != null)
                    {
                        hand.DettachGrabbableObject(leftGrabObject);
                        leftGrabObject = null;
                    }
                }
                else
                {
                    if (righttGrabObject != null)
                    {
                        hand.DettachGrabbableObject(righttGrabObject);
                        righttGrabObject = null;
                    }
                }

                
            }
        }
    }

    private void SetHandIsMoving(bool value, bool isLeft)
    {
        if (isLeft)
        {
            if (value && !isLeftHandMoving)
            {
                previousLeftHandPosition = controller.inputAction.FindActionMap("XRI LeftHand").FindAction("Position").ReadValue<Vector3>();
                leftHandDistance = 0;
            }
            
            isLeftHandMoving = value;
        }
        else
        {
            if (value && !isRightHandMoving)
            {
                previousRightHandPosition = controller.inputAction.FindActionMap("XRI RightHand").FindAction("Position").ReadValue<Vector3>();
                rightHandDistance = 0;
            }
            
            isRightHandMoving = value;
        }
    }

    public override void UpdateComponent()
    {
        float leftGripValue = controller.inputAction.FindActionMap("XRI LeftHand interaction").FindAction("Select Value").ReadValue<float>();
        float rightGripValue = controller.inputAction.FindActionMap("XRI RightHand interaction").FindAction("Select Value").ReadValue<float>();

        if (leftGripValue > 0)
        {
            SetGrip(controller.leftTrigger, true);

            Vector3 leftPosition = controller.inputAction.FindActionMap("XRI LeftHand").FindAction("Position").ReadValue<Vector3>();
            Vector3 leftVelocity = controller.inputAction.FindActionMap("XRI LeftHand").FindAction("Velocity").ReadValue<Vector3>();

            if (leftVelocity.magnitude > velocityTreshold)
            {
                //Debug.Log("LeftHand : " + leftVelocity.magnitude);
                SetHandIsMoving(true, true);
                leftHandDistance = Mathf.Abs(Vector3.Distance(previousLeftHandPosition, leftPosition));

                if (leftHandDistance >= distanceAccpetance)
                {
                    Vector3 leftHandDirection = (leftPosition - previousLeftHandPosition).normalized;

                    GrabbableAttachedObject leftAttachedObject = leftGrabObject as GrabbableAttachedObject;
                    
                    if (leftAttachedObject != null)
                    {
                        bool isDirCheck = leftAttachedObject.CheckDirToAttach(leftHandDirection, angleAccpetance);
                        Debug.Log("LeftHand : " + isDirCheck);
                        if (isDirCheck)
                        {
                            leftAttachedObject.SetIsAttached();
                        }
                    }
                }
            }
            else
            {
                SetHandIsMoving(false, true);
            }
        }
        else
        {
            SetGrip(controller.leftTrigger, false);
        }

        if (rightGripValue > 0)
        {
            SetGrip(controller.rightTrigger, true);

            Vector3 rightPosition = controller.inputAction.FindActionMap("XRI RightHand").FindAction("Position").ReadValue<Vector3>();
            Vector3 leftVelocity = controller.inputAction.FindActionMap("XRI RightHand").FindAction("Velocity").ReadValue<Vector3>();

            if (leftVelocity.magnitude > velocityTreshold)
            {
                //Debug.Log("RightHand : " + leftVelocity.magnitude);
                SetHandIsMoving(true, false);
                rightHandDistance = Mathf.Abs(Vector3.Distance(previousRightHandPosition, rightPosition));

                if (rightHandDistance >= distanceAccpetance)
                {
                    Vector3 rightHandDirection = (rightPosition - previousRightHandPosition).normalized;

                    GrabbableAttachedObject rightAttachedObject = righttGrabObject as GrabbableAttachedObject;

                    if (rightAttachedObject != null)
                    {
                        bool isDirCheck = rightAttachedObject.CheckDirToAttach(rightHandDirection, angleAccpetance);
                        Debug.Log("RightHand : " + isDirCheck);
                        if (isDirCheck)
                        {
                            rightAttachedObject.SetIsAttached();
                        }
                    }
                }
            }
            else
            {
                SetHandIsMoving(false, false);
            }
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
