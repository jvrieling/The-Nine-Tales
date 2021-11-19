using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemStack> items;
    public Item debugItem;
    public GameObject inventoryItemUI;

    private void Awake()
    {
        if (items == null)
        {
            items = new List<ItemStack>();
        }
    }
    [ContextMenu("Give Debug Item")]
    public void GiveDebugItem()
    {
        if (items == null) items = new List<ItemStack>();
        if (debugItem != null) AddItem(debugItem);
        else Debug.LogError("Failed to give debug item: there is no debug item set!");
    }

    [ContextMenu("Use Debug Item")]
    public void UseDebugItem()
    {
        if (items == null) items = new List<ItemStack>();
        if (debugItem != null) UseItem(debugItem);
        else Debug.LogError("Failed to use debug item: there is no debug item set!");
    }

    public void AddItem(Item item)
    {
        items.Add(new ItemStack(item));
    }

    public void AddItem(ItemStack item)
    {
        items.Add(item);
    }

    public bool UseItem(Item item)
    {
        int index = GetItemIndex(item);

        if (index == -1) return false;

        items[index].count -= 1;
        if (items[index].count < 1)
        {
            items.Remove(items[index]);
        }
        return true;
    }

    /// <summary>
    /// Checks if the inventory contains an item.
    /// </summary>
    /// <param name="item">The item you are checking if the inventory contains</param>
    /// <returns>the index in the inventory of the item, or -1 if the inventory does not contain the item.</returns>
    public int GetItemIndex(Item item)
    {
        return items.FindIndex(item.Equals);
    }
    public bool Contains(Item item)
    {
        foreach (ItemStack i in items)
        {
            if (i.Equals(item))
            {
                return true;
            }
        }
        return false;
    }
}
