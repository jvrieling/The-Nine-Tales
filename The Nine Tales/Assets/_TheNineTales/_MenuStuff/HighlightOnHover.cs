using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class HighlightOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
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
        Highlight();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hide();
    }
    public void OnSelect(BaseEventData eventData)
    {
        Highlight();
    }
    public void OnDeselect(BaseEventData eventData)
    {
        Hide();
    }

    private void Highlight()
    {
        img.sprite = hoverButton;
        if (normalTextImage != null) normalTextImage.SetActive(false);
        if (hoverTextImage != null) hoverTextImage.SetActive(true);
    }
    private void Hide()
    {
        img.sprite = normalButton;
        if (hoverTextImage != null) hoverTextImage.SetActive(false);
        if (normalTextImage != null) normalTextImage.SetActive(true);
        EventSystem i;
    }
}
