using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class GrabbableObject : MonoBehaviour
{
    public bool canBeGrab;
    public bool isGrab;
    public bool isKinematic;

    [SerializeField] public Rigidbody body;
    [SerializeField] protected XRGrabInteractable grabInteractable;
    protected XRInteractionManager interactionManager;
    protected XRDirectInteractor interactor;

    void Start()
    {
        InitializeObject();
        GameManager.StartNoGrav += InitNoGrav;
    }

    protected virtual void InitializeObject()
    {
        SetCanBeGrab(canBeGrab);
        grabInteractable.selectFilters.Add(new CanBeGrabbedFilter(this));
        interactionManager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
<<<<<<< HEAD
        isKinematic = false;
<<<<<<< Updated upstream
=======
        grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;

>>>>>>> Stashed changes
        gameObject.layer = LayerMask.NameToLayer("Grabbable");
=======
>>>>>>> parent of f328f69 (Merge branch 'Branche-dangereuse')
    }

    protected virtual void SetCanBeGrab(bool canBeGrab)
    {
        this.canBeGrab = canBeGrab;

        if (canBeGrab)
        {
            Debug.Log("SetCanGrab to True // " + isGrab);

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
            Debug.Log("SetCanGrab to False");
        }
    }

    public virtual void SetIsGrab(bool value, XRDirectInteractor interactor)
    {
        if (isGrab != value)
        {
            isGrab = value;

            if (value)
            {
                this.interactor = interactor;
            }
            else
            {
                this.interactor = null;
            }
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (isGrab)
        {
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InitNoGrav();
        }
    }


    private void InitNoGrav()
    {
        GetComponent<Rigidbody>().useGravity = false;
        Vector3 forceStartGrav = new Vector3(Random.Range(-0.5f,0.5f), Random.Range(2.5f,3), Random.Range(-0.5f, 0.5f));
        GetComponent<Rigidbody>().AddForce(forceStartGrav * Random.Range(0.008f,0.02f));
        Debug.Log("oui");
    }
}
