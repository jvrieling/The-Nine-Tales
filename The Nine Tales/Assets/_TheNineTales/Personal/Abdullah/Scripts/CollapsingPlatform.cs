using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    public Vector2 CollapseSpeed;
    public float CollapseTime = 1;
    Rigidbody2D RB;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
