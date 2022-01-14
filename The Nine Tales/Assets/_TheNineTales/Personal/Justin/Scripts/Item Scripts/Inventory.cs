using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static List<ItemStack> items;
    public Item debugItem;

    public Text inventoryText;
    public Image[] icons;

    private void Awake()
    {
        if (items == null)
        {
            items = new List<ItemStack>();
        }
    }

    private void Start()
    {
        UpdateUI();
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
        AddItem(new ItemStack(item));

        UpdateUI();
    }

    public void AddItem(ItemStack item)
    {
        int index = GetItemIndex(item);
        if (index == -1)
        {
            items.Add(item);
        } else
        {
            items[index].count += item.count;
        }

        UpdateUI();
    }

    public bool UseItem(Item item, int count = 1)
    {
        int index = GetItemIndex(item);

        if (index == -1) return false;

        RemoveItem(index, count);
        return true;
    }

    public void RemoveItem(Item item, int count)
    {
        int index = GetItemIndex(item);

        if (index == -1) return;
        RemoveItem(index, count);
    }
    public void RemoveItem(int index, int count)
    {
        items[index].count -= count;
        if (items[index].count < 1)
        {
            items.Remove(items[index]);
        }

        UpdateUI();
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
    public int GetItemIndex(ItemStack item)
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

    /// <summary>
    /// Checks if the inventory contains the item of a specified amount.
    /// </summary>
    /// <param name="item">The item you're looking for</param>
    /// <param name="minCount">The minimum number of the item you're loooking for</param>
    /// <param name="maxCount">The max number of the item you're looking for.</param>
    /// <returns>True if the inventory contains an item stack between the specified values. (both inclusive)</returns>
    public bool Contains(Item item, int minCount = 1, int maxCount = int.MaxValue)
    {
        int index = GetItemIndex(item);

        if(index >= 0)
        {
            return items[index].count >= minCount && items[index].count <= maxCount;
        }

        return false;
    }

    public void UpdateUI()
    {
        string inventoryStr = "";
        int j = 0;

        foreach(Image i in icons)
        {
            i.enabled = false;
        }

        foreach (ItemStack i in items)
        {
            inventoryStr += i.itemName + " x" + i.count + "\n";

            if (j < icons.Length)
            {
                icons[j].enabled = true;
                icons[j].sprite = i.icon;
                j++;
            }
        }

        inventoryText.text = inventoryStr;
    }
}
