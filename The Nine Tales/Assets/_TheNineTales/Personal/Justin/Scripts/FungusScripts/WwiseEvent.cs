using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;
using Fungus;

[CommandInfo("Wwise",
    "Post Event",
    "Posts a user-defined Wwise event.")]
public class WwiseEvent : Command
{
    [Tooltip("The Wwise event to post. You should be able to drag and drop from the Wwise window normally.")]
    public AK.Wwise.Event myEvent;
    [Tooltip("The game object to post this sound to. My understanding is that this is the object that is making the sound. If left empty, it will choose the Game Object that the Fungus flowchart component is on.")]
    public GameObject soundObject;

    public override void OnEnter()
    {
        if (soundObject == null) soundObject = gameObject;
        myEvent.Post(soundObject);
        Continue();
    }

    public override string GetSummary()
    {
        return (myEvent.Name != "") ? "Post Wwise event " + myEvent.Name : "No Wwise event selected!";
    }

    public override Color GetButtonColor()
    {
        return (myEvent != null) ? new Color32(0, 159, 218, 255) : new Color32(204, 106, 108, 255);
    }
}
