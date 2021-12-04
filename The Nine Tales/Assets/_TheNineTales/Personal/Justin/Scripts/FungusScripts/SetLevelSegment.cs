using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Level Tracker",
    "Set level segment",
    "Sets the the level and level segment to the provided number/character. Use to track the player's progress.")]
public class SetLevelSegment : Command
{
    public int level;
    public char segment;

    public override void OnEnter()
    {
        LevelTracker.SetLevelSegment(level, segment);
        Continue();
    }

    public override string GetSummary()
    {
        return level.ToString() + "-" + segment;
    }

    public override Color GetButtonColor()
    {
        return new Color32(189, 212, 188, 255);
    }
}
