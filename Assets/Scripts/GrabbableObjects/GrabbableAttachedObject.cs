using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableAttachedObject : GrabbableObject
{
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
<<<<<<< Updated upstream
    [SerializeField] protected GrabbableAttachTrigger trigger;
<<<<<<< HEAD
    [SerializeField] protected Collider triggerCollider;
=======
    [SerializeField] public GrabbableAttachTrigger trigger;
    [SerializeField] protected Collider TriggerCollider;
>>>>>>> Stashed changes
=======
>>>>>>> parent of f328f69 (Merge branch 'Branche-dangereuse')
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

        DirToDetach = (movementTransform[1].position - movementTransform[0].position).normalized;
        handsOffset = leftHandAttached.position - transform.position;
        grab = GameObject.Find("Camera Offset").GetComponent<PlayerGrab>();
        leftHandRenderer.enabled = false;
        rightHandRenderer.enabled = false;
<<<<<<< HEAD

<<<<<<< Updated upstream
        if (isFirstDisk || isLastDisk)
=======
        if (isFirstDisk && isLastDisk)
>>>>>>> Stashed changes
        {
            isKinematic = true;
        }
=======
>>>>>>> parent of f328f69 (Merge branch 'Branche-dangereuse')
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
            if (!trigger.isObjectInBox)
            {
                return false;
            }
        }

        return angle <= acceptanceAngle;
    }

<<<<<<< HEAD
    public bool IsInsideCollider()
    {
<<<<<<< Updated upstream
        if (triggerCollider == null)
=======
        if (TriggerCollider == null)
>>>>>>> Stashed changes
        {
            Debug.LogWarning("Aucun collider assign� !");
            return false;
        }

<<<<<<< Updated upstream
        Vector3 center = triggerCollider.bounds.center;
        Vector3 halfExtents = triggerCollider.bounds.extents;
        Quaternion rotation = triggerCollider.transform.rotation;
=======
        Vector3 center = TriggerCollider.bounds.center;
        Vector3 halfExtents = TriggerCollider.bounds.extents;
        Quaternion rotation = TriggerCollider.transform.rotation;
>>>>>>> Stashed changes

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

=======
>>>>>>> parent of f328f69 (Merge branch 'Branche-dangereuse')
    public virtual void SetIsAttached()
    {
        if (isSnap)
        {
            if (isAttached)
            {
                isAttached = false;
                needToBeAttached = false;
            }
            else
            {
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
        //Tween rotTween = transform.DORotate(rotToSnap, transitionTime);

        while (transform.position != posToSnap)
        {
            Debug.Log("aaaaaaaaaaaaaah");
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
                //rotTween = transform.DORotate(rotToSnap, newTime);
            }
            else
            {
                transform.position = posToSnap;
                transform.rotation = Quaternion.Euler(rotToSnap);
                posTween.Kill();
                //rotTween.Kill();
            }
        }

        posTween.Kill();
        //rotTween.Kill();
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

<<<<<<< HEAD
<<<<<<< Updated upstream
    protected override void Update()
    {
=======
    protected override void InitNoGrav()
    {
        base.InitNoGrav();
    }

    protected override void Update()
    {
>>>>>>> Stashed changes
        base.Update();

=======
    private void Update()
    {
>>>>>>> parent of f328f69 (Merge branch 'Branche-dangereuse')
        if (!isSnap && !isSnapping)
        {
            StartCoroutine(SnapTo());
        }
    }
}
