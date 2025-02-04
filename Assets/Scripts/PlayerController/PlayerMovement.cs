using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerComponent
{
    public InputActionProperty pinch;
    void Start()
    {
        
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
        float pinchValue = pinch.action.ReadValue<float>();

        Debug.Log(pinchValue);
    }
}
