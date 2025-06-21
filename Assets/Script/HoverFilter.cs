using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HoverFilter : MonoBehaviour,IXRHoverFilter
{
    public bool canProcess => isActiveAndEnabled;

    public bool Process(IXRHoverInteractor interactor, IXRHoverInteractable interactable)
    {
        if (interactor.transform.GetComponent<TagSet>() && interactable.transform.GetComponent<TagSet>())
            return interactable.transform.GetComponent<TagSet>().fliterTag == interactor.transform.GetComponent<TagSet>().fliterTag;
        else
            return false;
    }
}
