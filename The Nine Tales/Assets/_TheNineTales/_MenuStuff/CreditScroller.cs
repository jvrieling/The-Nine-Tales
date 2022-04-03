using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScroller : MonoBehaviour
{

    public float scrollSpeed;

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0f, scrollSpeed, 0f);
        

        if (this.transform.position.y >= 11000f) {
            SceneManager.LoadScene("P_sc_MainMenu");
        }
        
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            SceneManager.LoadScene("P_sc_MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            SceneManager.LoadScene("P_sc_MainMenu");
        }

    }
}
