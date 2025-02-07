using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableTool : GrabbableObject
{
    [SerializeField] public bool isBlowTorch;
    [SerializeField] BlowTorchTrigger trigger;
    [SerializeField] VisualEffect blowtorchFlame;
    void Start()
    {
        InitializeObject();
    }

    protected override void InitializeObject()
    {
        base.InitializeObject();

        gameObject.layer = LayerMask.NameToLayer("Tool");

        if (isBlowTorch)
        {
            blowtorchFlame.Stop();
        }
    }

    protected override void SetCanBeGrab(bool canBeGrab)
    {
        base.SetCanBeGrab(canBeGrab);
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

    protected override void Update()
    {
        base.Update();
    }

    protected override void InitNoGrav()
    {
        base.InitNoGrav();
    }

    public virtual void ActivateTool(bool isActive)
    {
        if (isBlowTorch)
        {
            if (isActive)
            {
                if (isBlowTorch && blowtorchFlame.aliveParticleCount < 0)
                {
                    blowtorchFlame.Play();
                }

                Debug.Log(isActive);
                if (trigger.diskCacheAttach != null)
                {
                    trigger.diskCacheAttach.SetActive(!trigger.diskCacheAttach.isActive);
                }
            }
            else
            {
                if (isBlowTorch && blowtorchFlame.aliveParticleCount > 0)
                {
                    blowtorchFlame.Stop();
                }
            }
        }
    }
}
