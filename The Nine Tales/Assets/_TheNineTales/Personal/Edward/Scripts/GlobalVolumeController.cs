using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeController : MonoBehaviour
{
    Vignette vignette;
    float VintMod = 2;
    public static GlobalVolumeController Singleton;

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        Volume volume = gameObject.GetComponent<Volume>();
        Vignette tmp;
        if (volume.profile.TryGet<Vignette>(out tmp))
        {
            vignette = tmp;
        }
    }

    public void CameraFadeDark(float _currentHp, float _totalHp)
    {
        float per = (_totalHp - _currentHp + VintMod) / (_totalHp + VintMod);
        vignette.intensity.value = per;
    }

    public void ResetCameraFade()
    {
        vignette.intensity.value = 0;
    }
}
