using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageImage : MonoBehaviour
{
    public static DamageImage DamageImageSingleton;
    SpriteRenderer DamageSprite;
    float MinAlpha = 0.2f;

    private void Awake()
    {
        DamageImageSingleton = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        DamageSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateAlpha(float currentHp, float totalHp)
    {
        if (currentHp >= totalHp)
        {
            ResetImage();
        }
        else
        {
            //float per = (_totalHp - _currentHp + VintMod) / (_totalHp + VintMod);
            float ColorAlpha = MinAlpha + ((totalHp - currentHp) / (totalHp -1) * (1 - MinAlpha));
            DamageSprite.color = new Color(1, 1, 1, ColorAlpha);
        }

    }

    public void ResetImage()
    {
        DamageSprite.color = new Color(1, 1, 1, 0); 
    }
}
