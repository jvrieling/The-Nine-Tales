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
        Debug.Log("text comp is " + objectiveText);
        if (objectiveText == null) objectiveText = GetComponentInChildren<Text>();
        if (objectiveText == null) objectiveText = GetComponent<Text>();
        Debug.Log("text comp NOW is " + objectiveText);
        objectiveText.text = "Current Objective: " + "\n" + newObjective;
    }

}
