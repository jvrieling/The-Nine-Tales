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
        normalTextImage.SetActive(false);
        hoverTextImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = normalButton;
        hoverTextImage.SetActive(false);
        normalTextImage.SetActive(true);
    }
}
