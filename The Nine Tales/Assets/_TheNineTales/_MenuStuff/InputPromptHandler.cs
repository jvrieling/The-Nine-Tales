using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InputPromptHandler : MonoBehaviour
{
    public Sprite keyboardButton, xboxButton;

    private Image img;

    void Start()
    {
        ChangeSprite();
    }
    private void OnValidate()
    {
        ChangeSprite();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash)) ChangeSprite();
        Invoke("ChangeSprite", 0.1f);
    }
    private void ChangeSprite()
    {
        if (img == null) img = GetComponent<Image>();

        if (InputTypeManager.isMouse) img.sprite = keyboardButton;
        else if (InputTypeManager.isCont) img.sprite = xboxButton;
    }
}
