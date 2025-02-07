using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PlayerController : MonoBehaviour
{

    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerGrab grab;
    public InputActionAsset inputAction;
    public Transform cameraMain;
    public ActionBasedController leftController;
    public ActionBasedController rightController;
    public XRDirectInteractor leftInteractor;
    public XRDirectInteractor rightInteractor;
    public HandTrigger leftTrigger;
    public HandTrigger rightTrigger;

    void Start()
    {
        if (movement != null)
        {
            movement.InitializeComponent(this);
        }

        if (grab != null)
        {
            grab.InitializeComponent(this);
        }

        inputAction = transform.parent.GetComponent<InputActionManager>().actionAssets[0];

        Physics.IgnoreLayerCollision(3, 6);
        Physics.IgnoreLayerCollision(3, 7);
        Physics.IgnoreLayerCollision(3, 8);
        Physics.IgnoreLayerCollision(3, 9);
        Physics.IgnoreLayerCollision(3, 10);
        Physics.IgnoreLayerCollision(7, 8);
        Physics.IgnoreLayerCollision(8, 9);
        Physics.IgnoreLayerCollision(8, 10);
    }

    void Update()
    {
        
    }
}
