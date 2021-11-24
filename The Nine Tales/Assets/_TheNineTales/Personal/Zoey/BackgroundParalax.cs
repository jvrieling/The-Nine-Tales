using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BackgroundParalax : MonoBehaviour
{
    private float length;
    private float startPosition;
    public GameObject cameraPos;
    public float parallaxEffect;

    public Vector2 positionAdjustments;

    public float yMoveMultiplier = 0.1f;

    //tracks whether or not the background element self translates
    public bool _selfMoving;
    public float _selfMovingRate;
    public float _totalDistanceSelfMoved;

    private void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // update the parallax effect
    private void FixedUpdate()
    {
        // track the distance we have moved
        float temp = (cameraPos.transform.position.x * (1 - parallaxEffect));

        float delta = cameraPos.transform.position.x * parallaxEffect;

        float selfMoveDelta = 0;

        // only calculate the self moving delta if this is a self moving paralax image
        if (_selfMoving) {
             selfMoveDelta = _selfMovingRate * Time.deltaTime;
             _totalDistanceSelfMoved = _totalDistanceSelfMoved + selfMoveDelta;
             startPosition = startPosition + selfMoveDelta;
            
        }

        transform.position = new Vector3(startPosition + delta + positionAdjustments.x, (cameraPos.transform.position.y + positionAdjustments.y) * yMoveMultiplier, transform.position.z);

        // if the distance we have self moved is greater then the length of the sprite then reset our sprite location
        if(Mathf.Abs(_totalDistanceSelfMoved) > length)
        {
            startPosition -= _totalDistanceSelfMoved;
            _totalDistanceSelfMoved = 0;
        }


        // adjust the position of this background image based off of its movement relative to where it started
        if(temp > startPosition + length)
        {
            startPosition += length;
        } else if(temp < startPosition - length)
        {
            startPosition -= length;
        }
    }

}
