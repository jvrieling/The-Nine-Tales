using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Flow",
    "Allow Skipping",
    "Allows the player to skip all blocks in this flowchart.")]
public class SkippableChart : Command
{
    [Tooltip("If the player skips this dialogue, the block 'block to execute' on this specified chart will be called. If left blank, it will call the block on this flowchart.")]
    public Flowchart ChartWhenSkipped;
    [Tooltip("If the player skips this dialogue, this block on the above specified chart will be called. Leave blank if you do NOT want any block to be called on skip.")]
    public string blockToExecute;
    public override void OnEnter()
    {
        if (blockToExecute != null && blockToExecute != "")
        {
            if (ChartWhenSkipped == null) ChartWhenSkipped = GetFlowchart();

            SkipManager.UpdateSkippableFlowchart(ChartWhenSkipped, ChartWhenSkipped, blockToExecute);
        }
        else
        {
            SkipManager.UpdateSkippableFlowchart(GetFlowchart());
        }
        Continue();
    }

    public override string GetSummary()
    {
        return "Allow skipping" + (!string.IsNullOrEmpty(blockToExecute) ? " to " + blockToExecute : "");
    }

    public override Color GetButtonColor()
    {
        return new Color32(255, 50, 50, 255);
    }
}
