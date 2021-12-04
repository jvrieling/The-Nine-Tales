using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Game State",
    "Set game state",
    "Sets the game state to a new given state. Effects Character Controller and Camera, maybe some other things.")]
public class SetGameState : Command
{
    public GameState newGameState;

    public override void OnEnter()
    {
        StateManager.SetState(newGameState);
        Continue();
    }

    public override string GetSummary()
    {
        return newGameState.ToString();
    }

    public override Color GetButtonColor()
    {
        return new Color32(255, 195, 11, 255);
    }
}
