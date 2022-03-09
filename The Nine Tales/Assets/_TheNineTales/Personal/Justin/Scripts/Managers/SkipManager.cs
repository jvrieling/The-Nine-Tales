using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SkipManager : MonoBehaviour
{
    public static Flowchart skippableFlowchart;
    private static Flowchart chartOnSkip;

    private void Awake()
    {
        chartOnSkip = GetComponent<Flowchart>();
    }

    public static void UpdateSkippableFlowchart(Flowchart chart)
    {
        skippableFlowchart = chart;
    }

    public static void ClearSkippableFlowchart()
    {
        skippableFlowchart = null;
    }

    public static void Skip()
    {
        if (skippableFlowchart != null)
        {
            skippableFlowchart.StopAllBlocks();
            chartOnSkip.ExecuteBlock("skip");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            Skip();
        }
    }
}
