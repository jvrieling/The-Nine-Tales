using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FadeLight : MonoBehaviour
{
    public Light2D lightToChange;
    public Color newColor = Color.white;
    public float newIntensity = 1;
    public float fadeDuration = 1;
    private float timePassed;
    private Color originalColor;
    private float originalIntensity;

    public System.Action callback;

    public void Initialize(Light2D light, float intensity, Color color, float fadeTime = 1)
    {
        lightToChange = light;
        newIntensity = intensity;
        newColor = color;
        fadeDuration = fadeTime;

        originalColor = lightToChange.color;
        originalIntensity = lightToChange.intensity;
    }

    void Update()
    {
        if (lightToChange != null)
        {
            timePassed += Time.deltaTime;

            lightToChange.color = Color.Lerp(originalColor, newColor, timePassed / fadeDuration);
            lightToChange.intensity = Mathf.Lerp(originalIntensity, newIntensity, timePassed / fadeDuration);

            if (timePassed >= fadeDuration)
            {
                lightToChange.color = newColor;
                lightToChange.intensity = newIntensity;
                if (callback != null) callback.Invoke();
                Destroy(this);
            }
        }
    }
}
