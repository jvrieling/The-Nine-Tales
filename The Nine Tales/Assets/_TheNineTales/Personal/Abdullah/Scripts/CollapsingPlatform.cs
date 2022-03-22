using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    public Vector2 CollapseSpeed;
    public float CollapseTime = 1;
    bool isFalling = false;
    Rigidbody2D RB;
    Vector3 OriginalPosition;
    float ResetTriggerHeight = -8;

    public AK.Wwise.Event collapseSound;


    void Start()
    {
        // Save original platform position to allow it to reset when player die or if it falls
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
        // only invoke a collapse if it isnt falling, to avoid invoking it on falling platfrom before resting
        if (!isFalling)
        {
            // Strt falling after delay
            Invoke("Collapse", CollapseTime);
        }
    }

    public void Collapse()
    {
        if(collapseSound.Name != "") collapseSound.Post(gameObject);
        RB.velocity = CollapseSpeed;
        isFalling = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

    }

    public void ResetToPosition()
    {
        RB.velocity = Vector3.zero;
        transform.position = OriginalPosition;
        isFalling = false;
    }
}
