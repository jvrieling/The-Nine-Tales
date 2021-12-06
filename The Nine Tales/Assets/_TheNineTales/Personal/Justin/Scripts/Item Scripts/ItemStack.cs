using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    public string itemName;
    public int count;
    public Sprite icon;
    public int id;

    public ItemStack(Item item)
    {
        itemName = item.itemName;
        count = 1;
        icon = item.icon;
        id = item.id;
    }

    public ItemStack(Item item, int stackCount)
    {
        itemName = item.itemName;
        count = stackCount;
        icon = item.icon;
        id = item.id;
    }

    public bool Equals(Item i)
    {
        return id == i.id;
    }
    public bool Equals(ItemStack i)
    {
        return id == i.id;
    }
}
