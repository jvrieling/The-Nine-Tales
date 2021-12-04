using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Level Tracker",
    "If last scene",
    "Checks if the last active scene was a specified name.")]
public class IfLastScene : Condition
{
    public string lastSceneName;
    protected override bool EvaluateCondition()
    {
        return LevelTracker.lastScene == lastSceneName;   
    }
    public override string GetSummary()
    {
        return " == " + lastSceneName;
    }
    public override Color GetButtonColor()
    {
        return new Color32(253, 253, 188, 255);
    }
}
