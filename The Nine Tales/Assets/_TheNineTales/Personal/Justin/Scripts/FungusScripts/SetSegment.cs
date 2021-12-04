using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Level Tracker",
    "Set segment",
    "Sets the the level segment to the provided character. Use to track the player's progress.")]

public class SetSegment : Command
{
    public char segment;

    public override void OnEnter()
    {
        LevelTracker.SetSegment(segment);
        Continue();
    }

    public override string GetSummary()
    {
        return segment.ToString();
    }

    public override Color GetButtonColor()
    {
        return new Color32(139, 169, 138, 255);
    }
}