using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SkipManager : MonoBehaviour
{
    public static Flowchart skippableFlowchart;
    private static Flowchart chartOnSkip;
    private static Flowchart chartToSkipTo;
    public Flowchart skipToDisplay;
    private static string block;
    public string skipToBlockDisplay;

    public static bool canSkip;

    private void Awake()
    {
        chartOnSkip = GetComponent<Flowchart>();
    }

    public static void UpdateSkippableFlowchart(Flowchart chart)
    {
        Debug.Log("updating " + chartOnSkip);
        skippableFlowchart = chart;
        canSkip = true;
    }
    public static void UpdateSkippableFlowchart(Flowchart chart, Flowchart skipTo, string blockToExecute)
    {
        Debug.Log("updating skip to block");
        skippableFlowchart = chart;
        chartToSkipTo = skipTo;
        block = blockToExecute;
        canSkip = true;
    }

    public static void ClearSkippableFlowchart()
    {
        skippableFlowchart = null;
        chartToSkipTo = null;
        block = null;
        canSkip = false;
    }

    public static void Skip()
    {
        if (skippableFlowchart != null)
        {
            skippableFlowchart.StopAllBlocks();

            if(chartToSkipTo != null)
            {
                chartToSkipTo.ExecuteBlock((block != null && block != "") ? block : "skip");
            } else
            {
                chartOnSkip.ExecuteBlock("skip");
            }
        }

        ClearSkippableFlowchart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            Skip();
        }
        skipToDisplay = chartToSkipTo;
        skipToBlockDisplay = block;
    }
}
