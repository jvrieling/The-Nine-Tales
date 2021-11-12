using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class HighlightOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int fontSizeBoost = 7;

    private Text buttonText;
    private void Awake()
    {
        buttonText = GetComponentInChildren<Text>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.fontSize += fontSizeBoost;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.fontSize -= fontSizeBoost;
    }
}
