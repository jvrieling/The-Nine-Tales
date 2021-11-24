using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityTracker : MonoBehaviour
{
    public SpriteRenderer sprite;
    
    public int slotNumber;

    public GameObject scriptObject;
    public BackgroundShifter trackingScript;

    private void Start()
    {
        trackingScript = scriptObject.GetComponent<BackgroundShifter>();
    }

    private void Update()
    {
        Color color = sprite.color;
        color.a = trackingScript.bulbOpacity[slotNumber];
        sprite.color = color;
    }
}
