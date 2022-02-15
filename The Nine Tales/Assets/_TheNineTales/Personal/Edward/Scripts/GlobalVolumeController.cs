using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeController : MonoBehaviour
{
    Vignette vignette;

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
        float per = _currentHp / _totalHp;
        vignette.intensity.value = per;
    }

    public void ResetCameraFade()
    {
        vignette.intensity.value = 0;
    }
}
