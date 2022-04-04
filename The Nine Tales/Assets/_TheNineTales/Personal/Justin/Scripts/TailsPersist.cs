using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailsPersist : MonoBehaviour
{
    public static bool[] tailsEnabled;

    private void Awake()
    {
        if (tailsEnabled == null)
        {
            tailsEnabled = new bool[9];
            tailsEnabled[0] = true;
        }
    }

    public void OnDisable()
    {
        for (int i = 0; i < 9; i++)
        {
            tailsEnabled[i] = transform.GetChild(i).gameObject.activeSelf;
        }
    }

    public void OnEnable()
    {
        for (int i = 0; i < 9; i++)
        {
            transform.GetChild(i).gameObject.SetActive(tailsEnabled[i]);
        }
    }
}
