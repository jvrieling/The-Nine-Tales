using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class StormFXController : MonoBehaviour
{
    VisualEffect storm;

    private void Awake()
    {
        storm = GetComponentInChildren<VisualEffect>();
    }

    public void PlayStorm()
    {
        storm.Play();
    }

    public void StopStorm()
    {
        storm.Stop();
    }
}
