using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[RequireComponent(typeof(Collider2D))]
public class TalkOnTrigger : MonoBehaviour
{
    public Flowchart flowchart;

    [Tooltip("The tag that will trigger the flowchart. Set to empty if you want any trigger collision to activate the flowchart.")]
    public string targetTag = "Player";

    [Tooltip("Set to false if you do NOT want to check for trigger collisions.")]
    public bool allowTriggers = true;
    [Tooltip("Set to true if you want to check for regular collisions.")]
    public bool allowCollisions = false;

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
            flowchart.ExecuteBlock("Trigger");
        }
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (allowCollisions && (targetTag == collision.gameObject.tag || string.IsNullOrEmpty(targetTag)))
        {
            flowchart.ExecuteBlock("Trigger");
        }
    }
}
