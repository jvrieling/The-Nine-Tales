using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    float moveSpeed = 2;
    float moveDirection = 1;
    float Range = 4;
    Vector3 OriginalPoisition;
    Vector3 TargetPoisition;

    void Start()
    {
        OriginalPoisition = transform.position;
        TargetPoisition = OriginalPoisition + new Vector3(0, Range, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.y < OriginalPoisition.y)
        {
            moveDirection = 1;
        }
        if (transform.position.y > TargetPoisition.y)
        {
            moveDirection = -1;
        }

        transform.position = transform.position + new Vector3(0, moveDirection * moveSpeed * Time.deltaTime, 0);
    }
}
