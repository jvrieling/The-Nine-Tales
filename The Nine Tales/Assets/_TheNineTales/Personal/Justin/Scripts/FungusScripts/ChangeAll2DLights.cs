using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.Rendering.Universal;

[CommandInfo("Lighting",
    "Set All 2D Lights",
    "Changes the colour and/or intensity of ALL 2D lights immediately under any Game Object with the 'LightContainer' tag.")]
public class ChangeAll2DLights : Command
{
    public Color newColor = Color.white;
    public float newIntensity = 1;
    public float fadeDuration = 1;

    public static List<Light2D> lights;

    private bool startResetDone;

    public void Start()
    {
        if (!startResetDone)
        {
            lights.Clear();
            lights = null;
            PopulateLightList();
            startResetDone = true;
        }
    }
    public override void OnValidate()
    {
        base.OnValidate();
        PopulateLightList();
    }
    public override void OnEnter()
    {
        bool needsTrim = false;
        foreach (Light2D i in lights)
        {
            if (i == null)
            {
                Debug.Log("Null light!");
                needsTrim = true;
                continue;
            }
            FadeLight fade = i.gameObject.AddComponent<FadeLight>();
            fade.Initialize(i, newIntensity, newColor, fadeDuration);
        }
        if (needsTrim) lights.TrimExcess();

        Continue();
    }

    public override string GetSummary()
    {
        return (lights.Count > 0) ? "Change " + lights.Count + " lights.": "No Light objects found!!";
    }

    public override Color GetButtonColor()
    {
        return new Color32(200, 200, 175, 255);
    }

    /// <summary>
    /// Populates the lights list with Lights from the scene
    /// </summary>
    /// <returns>True if lights were added to the list, false if no lights were found OR a list already existed.</returns>
    private void PopulateLightList()
    {
        if (lights == null)
        {
            lights = new List<Light2D>();

            //Fill the lights list with all 2D lights.
            GameObject[] lightObjects = GameObject.FindGameObjectsWithTag("Light");
            Light2D iLight;
            foreach (GameObject i in lightObjects)
            {
                iLight = i.GetComponent<Light2D>();
                if (iLight != null && !lights.Contains(iLight)) lights.Add(iLight);
                iLight = null;
            }
        }
    }
}
