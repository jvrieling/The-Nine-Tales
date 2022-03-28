using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public float delay = 3f;
    void OnEnable()
    {
        Destroy(gameObject, delay);
    }
}
