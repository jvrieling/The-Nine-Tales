using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Game State",
    "Undo game state",
    "Undoes the last change in game state.")]
public class UndoGameState : Command
{
    public override void OnEnter()
    {
        StateManager.UndoGameState();
        Continue();
    }

    public override string GetSummary()
    {
        return "Undo last game state";
    }

    public override Color GetButtonColor()
    {
        return new Color32(200, 165, 40, 255);
    }
}
