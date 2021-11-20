using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string interactionName = "Interact";

    public bool oneTimeUse;
    public bool destroyOnInteract;

    protected bool used;
    virtual public InteractableObject Interact(Interactor actor)
    {
        if (!used)
        {
            if (destroyOnInteract) Destroy(gameObject);
        }
        return this;
    }
    virtual public bool CanInteract() { return !used; }
}
