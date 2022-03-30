using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogueFade : MonoBehaviour
{
    private Flowchart chart;
    private bool showing;

    private void Awake()
    {
        chart = GetComponent<Flowchart>();
    }

    void Update()
    {
        if ((StateManager.CurrentGameState == GameState.Dialogue) && !showing)
        {
            chart.ExecuteBlock("show");
            showing = true;
        }
        else if (StateManager.CurrentGameState == GameState.Platforming && showing)
        {
            chart.ExecuteBlock("hide");
            showing = false;
        }
    }
}
