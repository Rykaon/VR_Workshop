using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit;

public class CanBeGrabbedFilter : IXRSelectFilter
{
    private GrabbableObject grabbableObject;

    public CanBeGrabbedFilter(GrabbableObject obj)
    {
        grabbableObject = obj;
    }

    // Indique si ce filtre peut être appliqué
    public bool canProcess => true;

    // Applique le filtre sur la sélection
    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        return grabbableObject.canBeGrab; // Retourne false si l'objet ne peut pas être saisi
    }
}
