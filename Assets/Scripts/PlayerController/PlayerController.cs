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
        //movement.InitializeComponent(this);
        grab.InitializeComponent(this);

        inputAction = transform.parent.GetComponent<InputActionManager>().actionAssets[0];
    }

    void Update()
    {
        
    }
}
