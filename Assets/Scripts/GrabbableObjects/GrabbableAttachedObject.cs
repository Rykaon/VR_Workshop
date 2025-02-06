using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrabbableAttachedObject : GrabbableObject
{
    [SerializeField] protected bool isAttached;
    protected bool isSnap;
    protected bool isSnapping;
    protected bool needToBeAttached;
    [SerializeField] protected Transform leftHandAttached;
    [SerializeField] protected Transform rightHandAttached;
    [SerializeField] protected Collider collision;
    [SerializeField] protected GrabbableAttachTrigger trigger;

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
        Vector3 posToSnap;
        Vector3 rotToSnap;

        if (needToBeAttached)
        {
            posToSnap = collision.transform.position - handsOffset;
            rotToSnap = collision.transform.rotation.eulerAngles;
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
                posToSnap = interactor.transform.rotation.eulerAngles;
            }
        }

        yield return null;
    }

    private void Update()
    {
        if (!isSnap && !isSnapping)
        {

        }
    }
}
