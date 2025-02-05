using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerComponent
{
    public Vector3 velocityPlayer;
    public bool isClimbing;
    public Rigidbody body;
    public InputActionProperty leftHandVelocity;
    public InputActionProperty rightHandVelocity;
    public float speedMultiplier;
    private string handUsed;
    public Transform playerCamera;
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public override void InitializeComponent(PlayerController controller)
    {
        base.InitializeComponent(controller);
    }

    public override void UpdateComponent()
    {

    }

    void Update()
    {

        if (handUsed == "LeftHand")
        {
            velocityPlayer = leftHandVelocity.action.ReadValue<Vector3>();
        }
        else if (handUsed == "RightHand")
        {
            velocityPlayer = rightHandVelocity.action.ReadValue<Vector3>();
        }


        if (isClimbing)
        {
          
            body.velocity = -velocityPlayer*speedMultiplier;
        }
    }


    public void StartClimb(string hand)
    {
        isClimbing = true;
        handUsed = hand;
    }

    public void StopClimbing()
    {
        isClimbing = false;
    }
}
