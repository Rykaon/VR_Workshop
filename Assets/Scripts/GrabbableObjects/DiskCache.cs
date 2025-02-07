using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DiskCache : GrabbableObject
{
    public bool isSnap = true;
    [SerializeField] private GrabbableAttachTrigger trigger;
    [SerializeField] public List<DiskCacheAttach> diskCacheAttaches;
    [SerializeField] public Collider collision;
    Tween posTween;
    Tween rotTween;
    Coroutine routine;
    Vector3 posToSnap;
    Vector3 rotToSnap;

    void Start()
    {
        InitializeObject();
    }

    public void Snap()
    {
        routine = StartCoroutine(SnapToAnchors());
    }

    private IEnumerator SnapToAnchors()
    {
        yield return null;
        posToSnap = trigger.transform.position;
        rotToSnap = trigger.transform.rotation.eulerAngles;
        posTween = transform.DOMove(posToSnap, 0.5f).SetEase(Ease.OutSine);
        rotTween = transform.DORotate(rotToSnap, 0.5f).SetEase(Ease.OutSine);

        yield return new WaitForSecondsRealtime(0.5f);

        body.isKinematic = true;
        isSnap = true;
    }

    public void Attach()
    {
        if (canBeGrab)
        {
            bool canBeAttach = false;

            foreach (DiskCacheAttach item in diskCacheAttaches)
            {
                if (item.isActive)
                {
                    canBeAttach = true;
                }
            }

            if (canBeAttach)
            {
                SetCanBeGrab(false);
            }
        }
        else
        {
            bool canBeDettach = true;

            foreach (DiskCacheAttach item in diskCacheAttaches)
            {
                if (item.isActive)
                {
                    canBeDettach = false;
                    break;
                }
            }

            if (canBeDettach)
            {
                SetCanBeGrab(true);
            }
        }
    }

    protected override void InitializeObject()
    {
        base.InitializeObject();

        canBeGrab = false;
        collision.isTrigger = true;
        trigger.isDiskCache = true;
        gameObject.layer = LayerMask.NameToLayer("DiskCache");
    }

    public override void SetIsGrab(bool value, XRDirectInteractor interactor)
    {
        if (isGrab != value)
        {
            isGrab = value;

            if (value)
            {
                this.interactor = interactor;
                
                if (posTween != null)
                {
                    posTween.Kill();
                    rotTween.Kill();
                    collision.isTrigger = false;
                }

                if (routine != null)
                {
                    collision.isTrigger = false;
                    StopCoroutine(routine);
                }
            }
            else
            {
                this.interactor = null;
            }
        }
    }

    protected override void SetCanBeGrab(bool canBeGrab)
    {
        this.canBeGrab = canBeGrab;

        if (canBeGrab)
        {
            Debug.Log("SetCanGrab to True // " + isGrab);
            body.isKinematic = false;
            collision.isTrigger = false;

            if (isGrab)
            {
                SelectEnterEventArgs enterEventArgs = new SelectEnterEventArgs();
                enterEventArgs.interactorObject = interactor;
                interactionManager.SelectEnter(interactor as IXRSelectInteractor, grabInteractable as IXRSelectInteractable);
                Debug.Log("ForceSelect");
            }
        }
        else
        {
            collision.isTrigger = false;
            Debug.Log("SetCanGrab to False");
        }
    }
}
