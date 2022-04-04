using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Player",
    "Set Facing",
    "Sets the direction the player is facing.")]
public class SetPlayerFacing : Command
{
    public Player.FacingDirection setFacingDirection;
    public override void OnEnter()
    {
        Player.FaceDirection(setFacingDirection);
        Continue();
    }

    public override string GetSummary()
    {
        return "Make the player face " + setFacingDirection;
    }

    public override Color GetButtonColor()
    {
        return new Color32(204, 148, 131, 255);
    }
}
