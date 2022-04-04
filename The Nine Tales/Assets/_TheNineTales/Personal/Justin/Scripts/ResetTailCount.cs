using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTailCount : MonoBehaviour
{

    void Start()
    {
        TailsPersist.tailsEnabled = new bool[9];
        TailsPersist.tailsEnabled[0] = true;
    }
}
