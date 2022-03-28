using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SkipManager : MonoBehaviour
{
    public static Flowchart skippableFlowchart;
    private static Flowchart chartOnSkip;
    private static Flowchart chartToSkipTo;
    private static string block;


    private void Awake()
    {
        chartOnSkip = GetComponent<Flowchart>();
    }

    public static void UpdateSkippableFlowchart(Flowchart chart)
    {
        Debug.Log("updating " + chartOnSkip);
        skippableFlowchart = chart;
    }
    public static void UpdateSkippableFlowchart(Flowchart chart, Flowchart skipTo, string blockToExecute)
    {
        Debug.Log("updating skip to block");
        skippableFlowchart = chart;
        chartToSkipTo = skipTo;
        block = blockToExecute;
    }

    public static void ClearSkippableFlowchart()
    {
        skippableFlowchart = null;
        chartToSkipTo = null;
        block = null;
    }

    public static void Skip()
    {
        if (skippableFlowchart != null)
        {
            skippableFlowchart.StopAllBlocks();

            if(chartToSkipTo != null)
            {
                Debug.Log("skipto " + chartToSkipTo);
                chartToSkipTo.ExecuteBlock((block != null && block != "") ? block : "skip");
            } else
            {
                Debug.Log("skip " + chartOnSkip);
                chartOnSkip.ExecuteBlock("skip");
            }
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
