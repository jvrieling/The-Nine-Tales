using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveTextUpdater : MonoBehaviour
{

    public Text objectiveText;

    void Start() {
        objectiveText = GetComponent<Text>();
        
    }

    public void UpdateText(string newObjective)
    {
        objectiveText.text = "Current Objective: " + "\n" + newObjective;
        

        
    }

}
