using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControllerButtonSelector : MonoBehaviour
{
    private EventSystem es;

    public GameObject defaultButtonMain, defaultButtonOptions;

    void Start()
    {
        es = GetComponent<EventSystem>();
    }

    void Update()
    {
        if(es.currentSelectedGameObject != null) if (!es.currentSelectedGameObject.activeInHierarchy) es.SetSelectedGameObject(null);

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            if (es.currentSelectedGameObject == null)
            {
                if (defaultButtonMain.activeInHierarchy) es.SetSelectedGameObject(defaultButtonMain);
                else es.SetSelectedGameObject(defaultButtonOptions);
            }
        }
    }
}
