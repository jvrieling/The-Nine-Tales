using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    public string name;
    public int count;
    public Sprite icon;
    public int id;

    public ItemStack(Item item)
    {
        name = item.name;
        count = 1;
        icon = item.icon;
        id = item.id;
    }

    public ItemStack(Item item, int i)
    {
        name = item.name;
        count = i;
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
