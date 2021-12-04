using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Player",
    "Set Player position",
    "Sets the player's position without wasting cpu on iTween.")]
public class SetPlayerPosition : Command
{
    private static GameObject player;

    public Transform position;

    public override void OnEnter()
    {
        if (player == null) GetPlayer();

        if (player != null)
        {
            if (position != null)
            {
                player.transform.position = position.position;
            } else { Debug.LogWarning("The player's position was supposed to be set by a flowchart on " + gameObject.name + " but the position was not provided! Didn't move the player."); }
        } else { Debug.LogWarning("Couldnt find the player in the SetPlayerPosition script!"); }
        Continue();
    }

    public override string GetSummary()
    {
        string p = "NO PLAYER FOUND";
        if(player!= null)
        {
            p = player.name;
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
        if(player == null)
        {
            GetPlayer();
        }
    }

    private void GetPlayer()
    {
       player = GameObject.FindGameObjectWithTag("Player");
    }
}
