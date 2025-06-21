using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SelectFilter :MonoBehaviour,IXRSelectFilter
{
    public bool canProcess => isActiveAndEnabled;

    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        if (interactor.transform.GetComponent<TagSet>() && interactable.transform.GetComponent<TagSet>())
            return interactable.transform.GetComponent<TagSet>().fliterTag == interactor.transform.GetComponent<TagSet>().fliterTag;
        else
            return false;
    }

}
