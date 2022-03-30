using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class HighlightOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject normalTextImage, hoverTextImage;
    public Sprite normalButton, hoverButton;

    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.sprite = hoverButton;
        if (normalTextImage != null) normalTextImage.SetActive(false);
        if (hoverTextImage != null) hoverTextImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = normalButton;
        if (hoverTextImage != null) hoverTextImage.SetActive(false);
        if (normalTextImage != null) normalTextImage.SetActive(true);
    }
}
