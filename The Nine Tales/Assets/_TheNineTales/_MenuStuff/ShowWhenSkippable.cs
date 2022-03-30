using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWhenSkippable : MonoBehaviour
{
    public GameObject holder;

    void Update()
    {
        if (SkipManager.canSkip && !holder.activeSelf)
        {
            holder.SetActive(true);
        }
        else if (!SkipManager.canSkip && holder.activeSelf)
        {
            holder.SetActive(false);
        }
    }
}
