using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Level Tracker",
                 "If level",
                 "Check if the level and segment match the player's current level & segment.")]
public class IfLevelSegment : Condition
{
    public int level = 1;
    public char segment = 'a';

    protected override bool EvaluateCondition()
    {
        return LevelTracker.CurrentLevelSegmentEquals(level, segment);
    }

    public override string GetSummary()
    {
        return "if player's current level == " + level + "-" + segment;
    }

    public override Color GetButtonColor()
    {
        return new Color32(189, 212, 188, 255);
    }    
}
