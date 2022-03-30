using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInputType : MonoBehaviour
{
    public void Toggle()
    {
        InputTypeManager.instance.ToggleInputType();
    }
}
