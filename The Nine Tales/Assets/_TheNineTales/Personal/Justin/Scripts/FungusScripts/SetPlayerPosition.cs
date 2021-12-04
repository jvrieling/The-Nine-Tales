using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Player",
    "Set Player position",
    "Sets the player's position without wasting cpu on iTween.")]
public class SetPlayerPosition : Command
{
    public Transform position;

    public override void OnEnter()
    {
        if (Player.player == null) GetPlayer();

            if (position != null)
            {
                Player.player.transform.position = position.position;
            } else { Debug.LogWarning("The player's position was supposed to be set by a flowchart on " + gameObject.name + " but the position was not provided! Didn't move the player."); }
        Continue();
    }

    public override string GetSummary()
    {
        string p = "NO PLAYER FOUND";
        if(Player.player != null)
        {
            p = Player.player.name;
        } else
        {
            GetPlayer();
        }
        string posName = "null";
        if (position != null) posName = position.gameObject.name;

        return "move " + p + " to " + posName;
    }

    public override Color GetButtonColor()
    {
        return new Color32(255, 180, 200, 255);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Player.player == null)
        {
            GetPlayer();
        }
    }

    private void GetPlayer()
    {

        Player.player = GameObject.FindGameObjectWithTag("Player");
    }
}
