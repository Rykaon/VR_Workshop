using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableAttachedObject : GrabbableObject
{
    public bool isDoor;
    public bool isFirstDisk;
    public bool isLastDisk;
    private bool hasBeenDetached = false;
    [SerializeField] protected bool isAttached;
    [SerializeField] protected bool isSnap;
    protected bool isSnapping;
    protected bool needToBeAttached;
    [SerializeField] protected Transform leftHandAttached;
    [SerializeField] protected Transform rightHandAttached;
    [SerializeField] protected Transform AttachPoint;
    [SerializeField] protected Renderer leftHandRenderer;
    [SerializeField] protected Renderer rightHandRenderer;
    [SerializeField] protected Collider collision;
    [SerializeField] protected GrabbableAttachTrigger trigger;
    [SerializeField] protected Collider triggerCollider;
    protected PlayerGrab grab;

    [SerializeField] protected List<Transform> movementTransform;
    protected Vector3 handsOffset;
    protected Vector3 DirToDetach;
    protected float movementAngleAcceptance = 25f;

    void Start()
    {
        InitializeObject();
    }

    protected override void InitializeObject()
    {
        base.InitializeObject();

        if (!isDoor)
        {
            gameObject.layer = LayerMask.NameToLayer("Disk");
        }

        DirToDetach = (movementTransform[1].position - movementTransform[0].position).normalized;
        handsOffset = leftHandAttached.position - transform.position;
        grab = GameObject.Find("Camera Offset").GetComponent<PlayerGrab>();
        leftHandRenderer.enabled = false;
        rightHandRenderer.enabled = false;

        if (isFirstDisk || isLastDisk)
        {
            isKinematic = true;
        }
    }

    public override void SetIsGrab(bool value, XRDirectInteractor interactor)
    {
        if (isGrab != value)
        {
            isGrab = value;

            if (value)
            {
                this.interactor = interactor;

                if (!canBeGrab)
                {
                    if (interactor == grab.controller.leftInteractor)
                    {
                        //grab.leftRenderer.enabled = false;
                        leftHandRenderer.enabled = true;
                    }
                    else
                    {
                        //grab.rightRenderer.enabled = false;
                        rightHandRenderer.enabled = true;
                    }
                }
            }
            else
            {
                //grab.leftRenderer.enabled = true;
                //grab.rightRenderer.enabled = true;
                leftHandRenderer.enabled = false;
                rightHandRenderer.enabled = false;
                this.interactor = null;
            }
        }
    }

    protected override void SetCanBeGrab(bool canBeGrab)
    {
        base.SetCanBeGrab(canBeGrab);
    }

    public virtual bool CheckDirToAttach(Vector3 DirToCheck, float acceptanceAngle)
    {
        Vector3 DirToMatch;

        if (isAttached)
        {
            DirToMatch = DirToDetach;
        }
        else
        {
            DirToMatch = -DirToDetach;
        }

        float angle = Mathf.Abs(Vector3.Angle(DirToMatch, DirToCheck));

        if (!isAttached)
        {
            if (!IsInsideCollider())
            {
                return false;
            }
        }

        return angle <= acceptanceAngle;
    }

    public bool IsInsideCollider()
    {
        if (triggerCollider == null)
        {
            Debug.LogWarning("Aucun collider assigné !");
            return false;
        }

        Vector3 center = triggerCollider.bounds.center;
        Vector3 halfExtents = triggerCollider.bounds.extents;
        Quaternion rotation = triggerCollider.transform.rotation;

        Collider[] detectedObjects = Physics.OverlapBox(center, halfExtents, rotation);

        foreach (Collider col in detectedObjects)
        {
            if (col.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }

    public virtual void SetIsAttached()
    {
        if (isSnap)
        {
            if (isAttached)
            {
                if (!hasBeenDetached)
                {
                    if (isDoor)
                    {
                        GameManager.instance.firstDisk.locked = false;
                    }
                    else if (isFirstDisk)
                    {
                        GameManager.instance.checkDiskStatus.UnistallDisk(GameManager.instance.firstDisk);
                    }
                    else if (isLastDisk)
                    {
                        GameManager.instance.checkDiskStatus.UnistallDisk(GameManager.instance.secondDisk);
                    }
                    else
                    {
                        for (int i = 0; i < GameManager.instance.allDiskInScene.Length; i++)
                        {
                            if (this == GameManager.instance.allDiskInScene[i].disk)
                            {
                                GameManager.instance.checkDiskStatus.UnistallDisk(GameManager.instance.allDiskInScene[i]);
                            }
                        }
                    }
                }

                isAttached = false;
                needToBeAttached = false;
            }
            else
            {
                if (hasBeenDetached)
                {
                    if (isFirstDisk)
                    {
                        GameManager.instance.checkDiskStatus.InstallDisk(GameManager.instance.firstDisk);
                    }
                    else if (isLastDisk)
                    {
                        GameManager.instance.checkDiskStatus.InstallDisk(GameManager.instance.secondDisk);
                    }
                    else
                    {
                        for (int i = 0; i < GameManager.instance.allDiskInScene.Length; i++)
                        {
                            if (this == GameManager.instance.allDiskInScene[i].disk)
                            {
                                GameManager.instance.checkDiskStatus.InstallDisk(GameManager.instance.allDiskInScene[i]);
                            }
                        }
                    }
                }

                isAttached = true;
                needToBeAttached = true;
            }


            isSnap = false;
            isSnapping = false;
        }
    }

    private IEnumerator SnapTo()
    {
        isSnapping = true;
        float transitionTime = 1f;
        float elapsedTime = 0f;
        Vector3 posToSnap;
        Vector3 rotToSnap;

        if (needToBeAttached)
        {
            posToSnap = AttachPoint.position - handsOffset;
            rotToSnap = AttachPoint.rotation.eulerAngles;
        }
        else
        {
            if (interactor == null)
            {
                yield break;
            }
            else
            {
                posToSnap = interactor.transform.position;
                rotToSnap = interactor.transform.rotation.eulerAngles;
            }
        }

        Tween posTween = transform.DOMove(posToSnap, transitionTime);
        Tween rotTween = transform.DORotate(rotToSnap, transitionTime);

        while (transform.position != posToSnap)
        {
            elapsedTime += Time.deltaTime;
            float newTime = transitionTime - elapsedTime;

            if (needToBeAttached)
            {
                posToSnap = AttachPoint.position - handsOffset;
                rotToSnap = AttachPoint.rotation.eulerAngles;
            }
            else
            {
                posToSnap = interactor.transform.position;
                rotToSnap = interactor.transform.rotation.eulerAngles;
            }

            if (newTime > 0f)
            {
                posTween = transform.DOMove(posToSnap, newTime);
                rotTween = transform.DORotate(rotToSnap, newTime);
            }
            else
            {
                transform.position = posToSnap;
                transform.rotation = Quaternion.Euler(rotToSnap);
                posTween.Kill();
                rotTween.Kill();
            }
        }

        posTween.Kill();
        rotTween.Kill();
        SetSnap();

        yield return null;
    }

    protected virtual void SetSnap()
    {
        isSnap = true;
        leftHandRenderer.enabled = false;
        rightHandRenderer.enabled = false;

        if (needToBeAttached)
        {
            isAttached = true;
            collision.isTrigger = false;
            body.isKinematic = false;
            body.useGravity = true;
            SetCanBeGrab(false);
        }
        else
        {
            isAttached = false;
            collision.isTrigger = false;
            body.isKinematic = false;
            body.useGravity = true;
            SetCanBeGrab(true);
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!isSnap && !isSnapping)
        {
            StartCoroutine(SnapTo());
        }
    }

    protected override void InitNoGrav()
    {
        base.InitNoGrav();
    }
}
