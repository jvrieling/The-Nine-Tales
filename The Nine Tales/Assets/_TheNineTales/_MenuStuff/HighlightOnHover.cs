using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class HighlightOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int fontSizeBoost = 7;

    public Color hoverColor, hoverColor2;
    private Color normalColor, normalColor2;
    private UIGradient gradient;

    private Animator an;

    private void Awake()
    {
        an = GetComponent<Animator>();
        /*gradient = GetComponent<UIGradient>();
        if(gradient != null)
        {
            normalColor = gradient.m_color1;
            normalColor2 = gradient.m_color2;
        }*/
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        an.SetBool("MouseOver", true);
        /*if(gradient != null)
        {
            gradient.m_color1 = hoverColor;
            gradient.m_color2 = hoverColor2;
        }*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        an.SetBool("MouseOver", false);
        /*if (gradient != null)
        {
            gradient.m_color1 = normalColor;
            gradient.m_color2 = normalColor2;
        }*/
    }
}
