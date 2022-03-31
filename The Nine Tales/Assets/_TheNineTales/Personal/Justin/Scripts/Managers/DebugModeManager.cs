using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;

public class DebugModeManager : MonoBehaviour
{
    public GameObject console;
    public Item[] givableItems;

    public static Item[] items;

    // Start is called before the first frame update
    void Start()
    {
        items = givableItems;
        DebugLogConsole.AddCommand<string>("give", "Gives an item", Give);
        DebugLogConsole.AddCommand<string, int>("give", "Gives an item", Give);
        DebugLogConsole.AddCommand<string, int, int>("check", "Checks if a plyer has an item between min and max (inclusive)", CheckInventory);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightBracket))
        {
            if (Input.GetKeyDown(KeyCode.LeftBracket))
            {
                console.SetActive(!console.activeSelf);
            }
        }
    }

    public static void CheckInventory(string item, int min, int max)
    {
        foreach (Item i in items)
        {
            if (i.itemName.Equals(item, System.StringComparison.CurrentCultureIgnoreCase) || i.id.ToString() == item)
            {
                int index = Player.inventory.GetItemIndex(i);
                if (index > 0)
                {
                    if (Player.inventory.Contains(i, min, max))
                    {
                        Debug.Log("Success: The player has between " + min + " and " + max + " of " + item + " (" + Inventory.items[index].count + ")");
                    }
                    else
                    {
                        Debug.Log("Fail: The player has " + item + ", but not an amount between " + min + " and " + max);
                    }
                }
                else
                {
                    Debug.Log("Fail: The player does not have any " + item + " on hand.");
                }
                return;
            }
        }

        Debug.LogWarning("No item name or id found matching " + item + ". Please ensure it is added to the \"Givable Items\" list in inspector before pressing play.");
    }
    public static void Give(string g)
    {
        if(g == "1-e")
        {
            Give("Cut Grass", 10);
            Give("Spices", 1);
        }
        Give(g, 1);
    }
    public static void Give(string g, int count)
    {
        foreach (Item i in items)
        {
            if (i.itemName.Equals(g, System.StringComparison.CurrentCultureIgnoreCase) || i.id.ToString() == g)
            {
                Player.inventory.AddItem(new ItemStack(i, count));
                return;
            }
        }

        Debug.LogWarning("No item name or id found matching " + g + ". Please ensure it is added to the \"Givable Items\" list in inspector before pressing play.");
    }
}
