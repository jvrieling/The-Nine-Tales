using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestory : MonoBehaviour
{
    private void Start()
    {
        Invoke("Destory", 2f);
    }

    private void Destory()
    {
        Object.Destroy(gameObject);
    }
}
