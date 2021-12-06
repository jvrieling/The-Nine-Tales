using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;

public class DebugModeManager : MonoBehaviour
{
    public Item[] givableItems;

    public static Item[] items;

    // Start is called before the first frame update
    void Start()
    {
        items = givableItems;
        DebugLogConsole.AddCommand<string>("give", "Gives an item", Give);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void Give(string g)
    {
        foreach (Item i in items)
        {
            if(i.itemName.Equals(g, System.StringComparison.CurrentCultureIgnoreCase) || i.id.ToString() == g)
            {
                Player.inventory.AddItem(i);
                return;
            }
        }

        Debug.LogWarning("No item name or id found matching " + g + ". Please ensure it is added to the \"Givable Items\" list in inspector before pressing play.");
    }
}
