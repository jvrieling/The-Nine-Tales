using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuObject;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenuObject.activeSelf)
            {
                pauseMenuObject.SetActive(true);
            } else
            {
                Unpause();
            }
        }
    }
    public void Unpause()
    {
        pauseMenuObject.SetActive(false);
    }
}
