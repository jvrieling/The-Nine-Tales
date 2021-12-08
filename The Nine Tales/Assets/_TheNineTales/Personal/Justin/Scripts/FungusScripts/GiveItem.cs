using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Player",
    "Give item",
    "Give an item to the player's inventory.")]
public class GiveItem : Command
{
    public Item itemToGive;
    public int countToGive = 1;

    public override void OnEnter()
    {
        Player.inventory.AddItem(new ItemStack(itemToGive, countToGive));
        Continue();
    }
    public override string GetSummary()
    {
        string item = "";
        if (itemToGive != null) item = itemToGive.itemName;
        return item + " x" + countToGive;
    }

    public override Color GetButtonColor()
    {
        return new Color32(128, 206, 225, 255);
    }
}
