using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Player",
                 "If Player Has Item",
                 "Check if the player has an item in their inventory.")]
public class IfPlayerHasItem : Condition
{
    public Item requiredItem;
    public int minCount = 1;
    public int maxCount = int.MaxValue;

    protected override bool EvaluateCondition()
    {
        return Player.inventory.Contains(requiredItem, minCount, maxCount);
    }
    public override string GetSummary()
    {
        return "if player has " + requiredItem + " in their inventory. (" + minCount + "-" + maxCount +")";
    }

    public override Color GetButtonColor()
    {
        return new Color32(189, 212, 188, 255);
    }
}
