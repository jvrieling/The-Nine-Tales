using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLerp : MonoBehaviour
{
    private float mouseXPercent, mouseYPercent;
    public Vector2 minOffset, maxOffset;

    void Update()
    {
        mouseXPercent = Input.mousePosition.x / Screen.width;
        mouseYPercent = Input.mousePosition.y / Screen.height;


        Vector3 newPos = new Vector3();

        newPos.x = Mathf.Lerp(minOffset.x, maxOffset.x, mouseXPercent);
        newPos.y = Mathf.Lerp(minOffset.y, maxOffset.y, mouseYPercent);

        transform.position = newPos;
    }
}
