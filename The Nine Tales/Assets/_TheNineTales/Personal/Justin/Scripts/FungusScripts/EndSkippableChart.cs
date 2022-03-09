using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Flow",
    "End Skipping",
    "Ends the allowance of skipping this flowchart. Can be used any time, but should always be used at the end of a skippable chart. I don't think you NEED to use this command but idk what will happen if you don't lol")]
public class EndSkippableChart : Command
{
    public override void OnEnter()
    {
        SkipManager.ClearSkippableFlowchart();
        Continue();
    }

    public override string GetSummary()
    {
        return "End skipping";
    }

    public override Color GetButtonColor()
    {
        return new Color32(255, 20, 20, 255);
    }
}
