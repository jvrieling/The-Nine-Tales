using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Singleton;
    public List<CollapsingPlatform> CollapsingPlatforms = new List<CollapsingPlatform>();


    private void Awake()
    {
        Singleton = this;

    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPlatforms()
    {
        foreach (CollapsingPlatform SelectedPlatform in CollapsingPlatforms)
        {

            SelectedPlatform.ResetToPosition();
        }
    }
}
