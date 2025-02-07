using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DiskCache : GrabbableObject
{
    public bool isSnap = true;
    public bool needToBeSnapped;
    [SerializeField] private GrabbableAttachedObject Disk;
    private XRGrabInteractable grabInteractableCopy;
    [SerializeField] private GrabHandDetection handDetection;
    [SerializeField] public GrabbableAttachTrigger trigger;
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
<<<<<<< Updated upstream
=======
                Debug.Log("yo");
>>>>>>> Stashed changes
                SetCanBeGrab(true);
            }
        }
    }

    protected override void InitializeObject()
    {
        base.InitializeObject();

<<<<<<< Updated upstream
        canBeGrab = false;
        collision.isTrigger = true;
        trigger.isDiskCache = true;
        gameObject.layer = LayerMask.NameToLayer("DiskCache");
=======
        if (trigger != null)
        {
            trigger.isDiskCache = true;
            trigger.diskCacheAttaches = diskCacheAttaches;
        }
        
        gameObject.layer = LayerMask.NameToLayer("DiskCache");
        grabInteractableCopy = grabInteractable;

        if (!canBeGrab)
        {
            Destroy(grabInteractable);
            Destroy(body);
            collision.isTrigger = true;
        }
>>>>>>> Stashed changes
    }

    public override void SetIsGrab(bool value, XRDirectInteractor interactor)
    {
        if (isGrab != value)
        {
            isGrab = value;

            if (value)
            {
                this.interactor = interactor;
                Debug.Log("lallalalalalalalalalalalal");
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

    protected void CopyXRGrabInteractable()
    {
        grabInteractable.interactionLayers = grabInteractableCopy.interactionLayers;
        grabInteractable.selectEntered = grabInteractableCopy.selectEntered;
        grabInteractable.selectExited = grabInteractableCopy.selectExited;
        grabInteractable.hoverEntered = grabInteractableCopy.hoverEntered;
        grabInteractable.hoverExited = grabInteractableCopy.hoverExited;
        grabInteractable.attachTransform = grabInteractableCopy.attachTransform;
        grabInteractable.interactionManager = grabInteractableCopy.interactionManager;
    }

    protected override void SetCanBeGrab(bool canBeGrab)
    {
        this.canBeGrab = canBeGrab;

        if (canBeGrab)
        {
            Debug.Log("SetCanGrab to True // " + isGrab);
            
            if (body == null)
            {
                
                body = gameObject.AddComponent<Rigidbody>();
                body.useGravity = true;
                body.isKinematic = false;
                grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
                handDetection.grabInteractable = grabInteractable;
                CopyXRGrabInteractable();
                /*grabInteractable.hoverEntered.AddListener((args) => handDetection.OnHoverEnter(args));
                grabInteractable.hoverExited.AddListener((args) => handDetection.OnHoverExit(args));*/
                grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;

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
                Debug.Log("yo");
                
                body.AddForce((Vector3.up + new Vector3(Random.value, 0, Random.value)) * 15f, ForceMode.Impulse);
                StartCoroutine(Boom());
            }

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
            Destroy(grabInteractable);
            Destroy(body);
            Debug.Log("SetCanGrab to False");
        }
    }

    protected IEnumerator Boom()
    {
        yield return new WaitForSecondsRealtime(0.35f);
        trigger.objectToCheck = null;
        Destroy(this);
    }

    protected override void InitNoGrav()
    {
        base.InitNoGrav();
    }

    protected override void Update()
    {
        base.Update();
    }
}
