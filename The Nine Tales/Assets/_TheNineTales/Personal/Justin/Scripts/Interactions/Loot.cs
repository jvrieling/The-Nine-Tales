using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : InteractableObject
{
    public Item item;
    public int itemCount;

    public override bool CanInteract()
    {
        if (oneTimeUse) return !used;
        else return true;
    }

    public override InteractableObject Interact(Interactor actor)
    {
        if ((oneTimeUse && !used) || !oneTimeUse)
        {
            Inventory inv = actor.GetComponent<Inventory>();

            inv.AddItem(new ItemStack(item, itemCount));

            used = true;
            if(destroyOnInteract) Destroy(gameObject);
        }
        return this;
    }
}
