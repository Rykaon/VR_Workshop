using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PlayerController : MonoBehaviour
{

    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerGrab grab;
    public InputActionAsset inputAction;
    public Transform cameraMain;
    public XRController leftController;
    public XRController rightController;

    void Start()
    {
        movement.InitializeComponent(this);
        grab.InitializeComponent(this);

        inputAction = transform.parent.GetComponent<InputActionManager>().actionAssets[0];
    }

    void Update()
    {
        
    }
}
