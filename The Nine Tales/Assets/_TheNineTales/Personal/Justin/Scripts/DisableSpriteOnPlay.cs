using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSpriteOnPlay : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
