using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Windows;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
public class PlayerMovement : PlayerComponent
{
    private Vector3 velocityPlayer;
    [HideInInspector] public bool isClimbing;
    public Rigidbody body;
    [SerializeField] private InputActionProperty leftHandVelocity;
    [SerializeField] private InputActionProperty rightHandVelocity;
    [SerializeField] private InputActionProperty turnCamera;
    [SerializeField] private float speedMultiplier;
    private string handUsed;
    public Transform playerCamera;
    private int numberOfTurn;

    public bool gravity;


    
    void Start()
    {
     
        turnCamera.action.started += ctx => GetDirection(ctx.ReadValue<Vector2>());
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

        body.useGravity = gravity;
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
            if(gravity) 
                velocityPlayer.y = 0;
            velocityPlayer = Quaternion.AngleAxis(45*numberOfTurn, Vector3.up) * velocityPlayer;
            body.velocity = -velocityPlayer*speedMultiplier;
            
        }
    }

    private void GetDirection(Vector2 inputF)
    {
        var cardinal = CardinalUtility.GetNearestCardinal(inputF);
        switch (cardinal)
        {
            case Cardinal.North:
                break;
            case Cardinal.South:
               
                break;
            case Cardinal.East:
                numberOfTurn++;
                break;
                
            case Cardinal.West:
                numberOfTurn--;

                break;
              
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
