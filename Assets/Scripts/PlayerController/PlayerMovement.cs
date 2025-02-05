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
    private Rigidbody body;
    [SerializeField] private InputActionProperty leftHandVelocity;
    [SerializeField] private InputActionProperty rightHandVelocity;
    [SerializeField] private InputActionProperty turnCamera;
    [SerializeField] private float speedMultiplier;
    private string handUsed;
    public Transform playerCamera;
    public TextMeshPro DebugText;
    private int numberOfTurn;


    
    void Start()
    {
        body = GetComponent<Rigidbody>();
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
            velocityPlayer = Quaternion.AngleAxis(45*numberOfTurn, Vector3.up) * velocityPlayer;
            body.velocity = -velocityPlayer*speedMultiplier;
            DebugText.text = (-velocityPlayer).ToString();
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
                numberOfTurn += 4;
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
