using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SoundMiddleMan : MonoBehaviour
{
    public void PlaySound(Character character)
    {
        Debug.Log("Playing sound for" + character.NameText);
    }
}
