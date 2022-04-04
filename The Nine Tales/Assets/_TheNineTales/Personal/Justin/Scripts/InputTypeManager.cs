using UnityEngine;
using System;
using UnityEngine.UI;

public class InputTypeManager : MonoBehaviour
{
    public Image statusImage;

    public Sprite keyboardImage, controllerImage;

    public static bool isMouse = true;
    public static bool isCont;

    public static InputTypeManager instance;

    private void Awake()
    {
        statusImage.sprite = keyboardImage;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Debug.Log("input manager already existed!");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            ToggleInputType();
        }
    }

    public void ToggleInputType()
    {
        if (statusImage == null) statusImage = GameObject.Find("InputStatusImage").GetComponent<Image>();
        if (isMouse)
        {
            Debug.Log("Input type changed to controller.");
            isMouse = false;
            isCont = true;

            statusImage.sprite = controllerImage;
        } else if (isCont)
        {
            Debug.Log("Input type changed to keyboard.");
            isCont = false;
            isMouse = true;

            statusImage.sprite = keyboardImage;
        }
    }
}
