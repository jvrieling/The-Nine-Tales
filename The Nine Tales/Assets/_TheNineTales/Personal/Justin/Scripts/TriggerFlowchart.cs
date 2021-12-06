using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[RequireComponent(typeof(Collider2D))]
public class TriggerFlowchart : MonoBehaviour
{
    public Flowchart flowchart;

    [Tooltip("Set to false if you want this to trigger EVERY time the target hits the collider.")]
    public bool oneTimeUse = true;
    public bool disableTriggerOnUse = false;

    [Tooltip("The tag that will trigger the flowchart. Set to empty if you want any trigger collision to activate the flowchart.")]
    public string targetTag = "Player";

    [Tooltip("Set to false if you do NOT want to check for trigger collisions.")]
    public bool allowTriggers = true;
    [Tooltip("Set to true if you want to check for regular collisions.")]
    public bool allowCollisions = false;

    private bool triggered;

    private void OnValidate()
    {
        if (disableTriggerOnUse) oneTimeUse = true;
    }
    void Start()
    {
        if (flowchart == null)
        {
            Debug.LogWarning("The flowchart for TalkOnTrigger on Game Object " + gameObject.name + " was not assigned! Trying to get one using GetComponent()." +
                "\nIdeally you should assign flowchart for this script, however if the flowchart is on the same object as the trigger it will work.");
            flowchart = GetComponent<Flowchart>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (allowTriggers && (targetTag == collision.gameObject.tag || string.IsNullOrEmpty(targetTag)))
        {
            StartTalking();
        }
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (allowCollisions && (targetTag == collision.gameObject.tag || string.IsNullOrEmpty(targetTag)))
        {
            StartTalking();
        }
    }

    public void StartTalking()
    {
        if (!triggered)
        {
            flowchart.ExecuteBlock("Trigger");
            triggered = true;
            if (disableTriggerOnUse) GetComponent<Collider2D>().enabled = false;
        }
    }
}
