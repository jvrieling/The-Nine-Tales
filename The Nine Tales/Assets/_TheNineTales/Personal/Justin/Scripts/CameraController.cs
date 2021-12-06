using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool runInEditor;
    [Tooltip("The GameObject to follow around")]
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float accuracy = 1f;

    public Vector3 offset;
    public bool calculateOffsetOnStart = true;

    public float platformingCameraSize;
    public float narrativeCameraSize = 5.3f;
    public float zoomTime = 0.7f;
    public AnimationCurve zoomCurve;

    private float zoomTimer;
    private float targetSize;
    private float startingSize;
    private bool zooming;

    [Tooltip("set to true if the camera is currently zoomed in to the narrative camera size. Only affects anything if one of the camera sizes are not set.")]
    public bool zoomedIn;

    private bool follow = true;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (zoomedIn && narrativeCameraSize == 0) narrativeCameraSize = cam.orthographicSize;
        else if (!zoomedIn && platformingCameraSize == 0) platformingCameraSize = cam.orthographicSize;
    }
    private void OnValidate()
    {
        runInEditMode = runInEditor;
    }

    public void Update()
    {
        if (follow)
        {
            Vector3 desiredPosition = target.position + offset;
            if (Vector3.Distance(transform.position, desiredPosition) > accuracy)
            {
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
        }

        if (zooming)
        {
            zoomTimer = Mathf.Clamp(zoomTimer + Time.deltaTime, 0, zoomTime);

            cam.orthographicSize = Mathf.Lerp(startingSize, targetSize, zoomCurve.Evaluate(zoomTimer / zoomTime));

            if (zoomTimer >= zoomTime) zooming = false;
        }
    }

    [ContextMenu("Zoom Out")]
    public void ZoomOut()
    {
        SetCameraZoom(platformingCameraSize);
    }
    [ContextMenu("Zoom In")]
    public void ZoomIn()
    {
        SetCameraZoom(narrativeCameraSize);
    }

    public void SetCameraZoom(bool zoomIn)
    {
        if (zoomIn) SetCameraZoom(narrativeCameraSize);
        else SetCameraZoom(platformingCameraSize);
    }

    public void SetCameraZoom(float zoom)
    {
        targetSize = zoom;
        startingSize = cam.orthographicSize;
        zooming = true;
        zoomTimer = 0;
    }
    public void SetCameraFollow(bool shouldFollowTarget)
    {
        follow = shouldFollowTarget;
    }
}
