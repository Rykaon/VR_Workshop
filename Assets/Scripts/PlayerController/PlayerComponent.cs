using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    protected PlayerController controller;

    void Start()
    {
        
    }

    virtual public void InitializeComponent(PlayerController controller)
    {
        this.controller = controller;
    }

    virtual public void UpdateComponent()
    {

    }

    void Update()
    {
        
    }
}
