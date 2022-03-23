using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SoundMiddleMan : MonoBehaviour
{
    public AK.Wwise.Event PenScribbleSound;
    public void PlaySound()
    {
        PenScribbleSound.Post(Player.player);   
    }
}
