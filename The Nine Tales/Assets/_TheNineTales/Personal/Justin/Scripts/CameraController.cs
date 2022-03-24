using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool runInEditor;
    [Tooltip("The GameObject to follow around")]
    public Transform target;
    public float smoothSpeed = 0.125f;

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

    public float turnSmoothSpeed = 0.05f;
    private Vector3 turnOffset;
    private Vector3 turnPos;

    public GameObject stormVFX;

    public bool shouldZoom = true;
    public bool shouldFollow = true;

    private Camera cam;

    //UNUSED: See line 85.
    //public float accuracy = 1f;
    //private bool gettingInput = false;
    //private float accuracyUpTimer;
    //private float accuracyDownTimer;

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

    public void Update()
    {
        if (shouldFollow)
        {
            //Reset values
            Vector3 desiredPosition;
            Vector3 targetPos = target.position;
            turnOffset = Vector3.zero;

            //-----Calculate the adjusted position------
            //Adjusts the camera's target position so it isn't perfectly centered on the player

            //Camera targets putting the player on the upper third of the screen if the S key is pressed.
            int upperOrLowerThird = Input.GetKey(KeyCode.S) ? -3 : 3;
            turnOffset += new Vector3(0, cam.orthographicSize / upperOrLowerThird, 0);

            //Put the camera a little ahead of the player in the direction they are facing
            SpriteRenderer playerSprite = StateManager.cc.GetComponent<SpriteRenderer>();
            if (playerSprite.flipX)
            {
                turnOffset += new Vector3(cam.orthographicSize / 3, 0, 0);
            }
            else
            {
                turnOffset += new Vector3(-cam.orthographicSize / 3, 0, 0);
            }

            //Lerp the adjusted position to make it smoother, especially when turning around.
            turnPos = Vector3.Lerp(turnPos, turnOffset, turnSmoothSpeed);

            //Move the camera towards the final destination.
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPos + turnPos, smoothSpeed);
            desiredPosition = smoothedPosition;
            desiredPosition.z = -10; //Ensure the camera's z stays at -10.
            Vector3 temp = transform.position;
            temp.z = target.position.z;

            //Finally, move the camera.
            transform.position = desiredPosition;

            //CODE MAKING USE OF ACCURACY
            //Makes the camera only follow the player if they start heading too far from the camera.
            //Currently too jarring - needs code added to make it smoother.
            /*if (Mathf.Abs(InputTracker.GetDirectionalInput().x) > 0)
            {
                if (Vector3.Distance(targetPos + turnOffset, temp) > accuracy)
                {
                    accuracy = 0.1f;
                }
                accuracyUpTimer = 0;
                gettingInput = true;
            }
            else
            {
                if (accuracyUpTimer > 1)
                {
                    accuracy = 5;
                }
                else
                {
                    accuracyUpTimer += Time.deltaTime;
                }
            }

            //Finally, actually move the camera.
            if (Vector3.Distance(targetPos + turnOffset, temp) > accuracy) transform.position = desiredPosition;
            */
        }
        if (shouldZoom)
        {
            if (zooming)
            {
                zoomTimer = Mathf.Clamp(zoomTimer + Time.deltaTime, 0, zoomTime);

                cam.orthographicSize = Mathf.Lerp(startingSize, targetSize, zoomCurve.Evaluate(zoomTimer / zoomTime));

                if (zoomTimer >= zoomTime) zooming = false;
            }

            if (zoomedIn)
            {
                if (stormVFX != null) stormVFX.transform.localPosition = new Vector3(0, -0.82f, 0);
            }
            else
            {
                if (stormVFX != null) stormVFX.transform.localPosition = Vector3.zero;
            }
        }

    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(turnOffset + ((target != null) ? target.transform.position : Vector3.zero), 0.3f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(turnPos + ((target != null) ? target.transform.position : Vector3.zero), 0.3f);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(turnOffset + ((target != null) ? target.transform.position : Vector3.zero), 0.3f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(target.position, 0.3f);
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
        zoomedIn = zoomIn;
        if (zoomIn)
        {
            SetCameraZoom(narrativeCameraSize);
        }
        else
        {
            SetCameraZoom(platformingCameraSize);
        }
    }

    public void SetCameraZoom(float zoom)
    {
        targetSize = zoom;
        startingSize = cam.orthographicSize;
        zooming = true;
        zoomTimer = 0;
    }
    public void SetShouldZoom(bool zoom)
    {
        shouldZoom = zoom;
        shouldFollow = zoom;
    }
}
