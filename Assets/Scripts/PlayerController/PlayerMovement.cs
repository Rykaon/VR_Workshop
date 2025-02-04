using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerComponent
{
    public InputActionProperty grab;

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
        float gripValue = grab.action.ReadValue<float>();
        if (gripValue >= 1)
        {
            Debug.Log("Pressure");
        }
    }
}
