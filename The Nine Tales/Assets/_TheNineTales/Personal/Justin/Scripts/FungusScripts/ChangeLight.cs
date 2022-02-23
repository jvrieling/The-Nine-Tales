using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Fungus;

[CommandInfo("Lighting",
    "Set Light 2D",
    "Changes the colour and/or intensity of the selected Light 2D")]
public class ChangeLight : Command
{
    public Light2D lightToChange;
    public Color newColor = Color.white;
    public float newIntensity = 1;
    public float fadeDuration = 1;

    public override void OnEnter()
    {
        FadeLight fade = lightToChange.gameObject.AddComponent<FadeLight>();
        fade.Initialize(lightToChange, newIntensity, newColor, fadeDuration);
        //lightToChange.color = newColor;
        //lightToChange.intensity = newIntensity;
        Continue();
    }

    public override string GetSummary()
    {
        return (lightToChange != null) ? "Change " + lightToChange.name : "No Light2D set!";
    }

    public override Color GetButtonColor()
    {
        return new Color32(255, 255, 224, 255);
    }
}
