using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInputType : MonoBehaviour
{
    public void Toggle()
    {
        Debug.Log(InputTypeManager.instance);
        InputTypeManager.instance.ToggleInputType();
    }
}
