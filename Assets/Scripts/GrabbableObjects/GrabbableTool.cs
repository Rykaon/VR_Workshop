using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableTool : GrabbableObject
{
    void Start()
    {
        InitializeObject();
    }

    protected override void InitializeObject()
    {
        base.InitializeObject();
    }

    protected override void SetCanBeGrab(bool canBeGrab)
    {
        this.canBeGrab = canBeGrab;
    }

    public override void SetIsGrab(bool value, XRDirectInteractor interactor)
    {
        base.SetIsGrab(value, interactor);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (isGrab)
        {

        }
    }
}
