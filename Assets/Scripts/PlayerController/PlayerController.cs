using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerGrab grab;

    void Start()
    {
        movement.InitializeComponent(this);
        grab.InitializeComponent(this);
    }

    void Update()
    {
        
    }
}
