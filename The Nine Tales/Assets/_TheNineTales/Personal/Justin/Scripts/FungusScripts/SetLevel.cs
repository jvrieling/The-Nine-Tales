using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Level Tracker",
    "Set level",
    "Sets the the level to the provided number. Use to track the player's progress.")]
public class SetLevel : Command
{
    public int level;

    public override void OnEnter()
    {
        LevelTracker.SetLevel(level);
        Continue();
    }

    public override string GetSummary()
    {
        return level.ToString();
    }

    public override Color GetButtonColor()
    {
        return new Color32(189, 212, 188, 255);
    }
}
