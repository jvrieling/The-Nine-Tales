using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashStateVisualizer : MonoBehaviour
{
    public Material desaturateMaterial;
    public float fadeTime = 0.5f;

    private Player player;

    private float saturation, fadeTimer;
    private bool done;

    private void Start()
    {
        player = Player.player.GetComponent<Player>();
    }

    public void Update()
    {
        if (fadeTimer < fadeTime && player.CanDash && !done)
        {
            fadeTimer += Time.deltaTime;

            saturation = Mathf.Clamp(Mathf.Lerp(0, 1, fadeTimer / fadeTime), 0, 1);

            if (saturation >= 1) done = true;
        }
        else if (!player.CanDash)
        {
            saturation = 0;
            fadeTimer = 0;
            done = false;
        }

        desaturateMaterial.SetFloat("_Saturation", saturation);
    }
}
