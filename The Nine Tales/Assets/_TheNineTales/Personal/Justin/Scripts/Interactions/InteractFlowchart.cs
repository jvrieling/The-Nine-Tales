using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[RequireComponent(typeof(Flowchart))]
public class InteractFlowchart : InteractableObject
{
    public Flowchart flowchart;

    public string blockToExecute = "OnInteract";

    private void OnValidate()
    {
        if (flowchart == null) flowchart = GetComponent<Flowchart>();
    }

    override public InteractableObject Interact(Interactor actor)
    {
        if (!used)
        {
            if(oneTimeUse) used = true;
            flowchart.ExecuteBlock(blockToExecute);
            if (destroyOnInteract) Destroy(gameObject);
        }
        return this;
    }
    override public bool CanInteract()
    {
        return !used;
    }
}
