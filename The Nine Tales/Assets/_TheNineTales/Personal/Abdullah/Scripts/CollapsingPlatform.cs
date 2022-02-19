using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    public Vector2 CollapseSpeed;
    public float CollapseTime = 1;
    Rigidbody2D RB;
    Vector3 OriginalPosition;
    float ResetTriggerHeight = -8;


    void Start()
    {
        OriginalPosition = transform.position;
        RB = GetComponent<Rigidbody2D>();
        PlatformManager.Singleton.CollapsingPlatforms.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        // Rest platfrom if it falls too low
        if(transform.position.y < ResetTriggerHeight)
        {
            ResetToPosition();
        }
    }

    public void PrepCollapse()
    {
        Invoke("Collapse", CollapseTime);
    }

    public void Collapse()
    {
        RB.velocity = CollapseSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

    }

    public void ResetToPosition()
    {
        RB.velocity = Vector3.zero;
        transform.position = OriginalPosition;
    }
}
