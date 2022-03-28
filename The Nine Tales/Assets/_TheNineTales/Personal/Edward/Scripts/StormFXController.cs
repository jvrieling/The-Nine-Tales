using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class StormFXController : MonoBehaviour
{
    public VisualEffect storm;
    public VisualEffect thunder;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PlayThunder();
    }

    public void PlayStorm()
    {
        storm.Play();
    }

    public void StopStorm()
    {
        storm.Stop();
    }

    public void PlayThunder()
    {
        thunder.Stop();
        thunder.Play();
    }
}
