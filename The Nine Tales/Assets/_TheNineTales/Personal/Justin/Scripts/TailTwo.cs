using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailTwo : MonoBehaviour
{
    [Header("General Settings")]
    public int length = 6;
    public float targetDist = 0.25f;
    public float smoothSpeed = 0.02f;

    [Header("Wiggle Settings")]
    public float wiggleSpeed = 3;
    public float wiggleMagnitude = 10;
    public float velocitySpeedModifier = 0.5f;

    [Header("References")]
    public Transform tailBase;
    public LineRenderer lineRend;
    public Transform targetDir;
    public SpriteRenderer sr;
    public Player characterController;

    private int lastLength;
    private Vector3[] segments;
    private Vector3[] segmentV;
    private Quaternion startingRotation;
    private float originalWiggleSpeed;
    private float originalWiggleMagnitude;

    private void Start()
    {
        lineRend.positionCount = length;
        segments = new Vector3[length];
        segmentV = new Vector3[length];
        startingRotation = transform.rotation;
        originalWiggleSpeed = wiggleSpeed;
        originalWiggleMagnitude = wiggleMagnitude;
    }
    private void Reset()
    {
        OnValidate();
    }
    private void OnValidate()
    {
        if (lineRend == null) lineRend = GetComponentInParent<LineRenderer>();
        if (tailBase == null) tailBase = transform;
        if (sr == null) sr = GetComponentInParent<SpriteRenderer>();
        if (targetDir == null) targetDir = transform.GetChild(0);
        if (characterController == null) characterController = GetComponentInParent<Player>();

        if (lastLength != length)
        {
            lineRend.positionCount = length;
            segments = new Vector3[length];
            segmentV = new Vector3[length];
            lastLength = length;
        }
    }
    private void Update()
    {
        //Modify the tail wiggling based on the current velocity in the player controller.
        wiggleSpeed = originalWiggleSpeed + Mathf.Abs(characterController.m_PlayerVelocity.x) * velocitySpeedModifier;
        wiggleMagnitude = originalWiggleMagnitude - Mathf.Abs(characterController.m_PlayerVelocity.x) * velocitySpeedModifier;

        //Wiggle the tail by moving the rotation in a sine wave.
        targetDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        //Flip the target direction if the sprite is flipped.
        //Note: The sprite gets flipped by the character controller.
        if (sr.flipX)
        {
            tailBase.rotation = startingRotation * Quaternion.Euler(0, 0, 180);
        }
        else
        {
            tailBase.rotation = startingRotation * Quaternion.Euler(0, 0, 0);
        }

        segments[0] = targetDir.position;

        for (int i = 1; i < segments.Length; i++)
        {
            Vector3 targetPos = segments[i - 1] + (segments[i] - segments[i - 1]).normalized * targetDist;
            segments[i] = Vector3.SmoothDamp(segments[i], targetPos, ref segmentV[i], smoothSpeed);
        }

        lineRend.SetPositions(segments);
    }
}
