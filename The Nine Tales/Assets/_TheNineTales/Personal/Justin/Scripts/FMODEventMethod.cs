using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD.Studio;
using FMODUnityResonance;

public class FMODEventMethod : MonoBehaviour
{
    public EventReference @event;

    public void PlayEvent()
    {
        //TODO Play the event here 
        FMODUnity.RuntimeManager.CreateInstance(@event);
    }
}
