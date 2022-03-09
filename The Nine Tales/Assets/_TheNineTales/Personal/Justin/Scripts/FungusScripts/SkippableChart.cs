using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Flow",
    "Allow Skipping",
    "Allows the player to skip all blocks in this flowchart.")]
public class SkippableChart : Command
{
    public override void OnEnter()
    {
        SkipManager.UpdateSkippableFlowchart(GetFlowchart());
        Continue();
    }

    public override string GetSummary()
    {
        return "Allow skipping";
    }

    public override Color GetButtonColor()
    {
        return new Color32(255, 50, 50, 255);
    }
}
