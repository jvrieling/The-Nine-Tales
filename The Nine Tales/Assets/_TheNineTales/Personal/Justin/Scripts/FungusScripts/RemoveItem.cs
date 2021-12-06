using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Player",
    "Remove item",
    "Removes an item from the player's inventory.")]
public class RemoveItem : Command
{
    public Item itemToRemove;
    public int countToRemove = 1;

    public override void OnEnter()
    {
        Player.inventory.RemoveItem(itemToRemove, countToRemove);
        Continue();
    }
    public override string GetSummary()
    {
        string item = "";
        if (itemToRemove != null) item = itemToRemove.itemName;
        return item + " x" + countToRemove;
    }

    public override Color GetButtonColor()
    {
        return new Color32(128, 206, 225, 255);
    }
}
