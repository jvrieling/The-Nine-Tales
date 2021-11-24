using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionPlatform : MonoBehaviour
{
    private SpriteRenderer sr;
    private BoxCollider2D bc2D;

    public bool collisionOn;
    [SerializeField] private bool triggered = false;
    [SerializeField] private bool coroutineRunning = false;
    public float fadeDuration;
    [Range(0f, 1f)]
    public float startAlphaValue;
    [Range(0f, 1f)]
    public float endAlphaValue;
    


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, startAlphaValue);
        if (startAlphaValue == 0f) {
            startAlphaValue = .1f;
        }

        bc2D = GetComponent<BoxCollider2D>();
        if (!collisionOn && !bc2D.isTrigger)
        {
            bc2D.isTrigger = true;
        }
    }

    public void Reveal() {
        Debug.Log("message recieved");
        if (!coroutineRunning)
        {
            if (!triggered)
            {
                triggered = true;
                StartCoroutine(FadeObject(fadeDuration, endAlphaValue));
                
            }
            else if(triggered && collisionOn)
            {
                triggered = false;
                StartCoroutine(FadeObject(fadeDuration, startAlphaValue));
                
            }
        }
       
       
    }

    IEnumerator FadeObject(float duration, float alphaEnd) {
        coroutineRunning = true;
        if (collisionOn && bc2D.enabled && triggered)
        {
            bc2D.isTrigger = false;
        }
        else if (collisionOn && bc2D.enabled && !triggered) {
            bc2D.isTrigger = true;
        }
        else
        {
            bc2D.enabled = false;
        }
        for (float t = 0; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;
            sr.color = Color.Lerp(sr.color, new Color(sr.color.r, sr.color.g, sr.color.b, alphaEnd), normalizedTime);
            yield return null;
        }
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alphaEnd);
       
        coroutineRunning = false;

    }
}
   
