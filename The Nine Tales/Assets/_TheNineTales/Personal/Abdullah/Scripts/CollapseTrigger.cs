using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseTrigger : MonoBehaviour
{
    public CollapsingPlatform TargetPlatform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            TargetPlatform.PrepCollapse();
        }
    }
}
