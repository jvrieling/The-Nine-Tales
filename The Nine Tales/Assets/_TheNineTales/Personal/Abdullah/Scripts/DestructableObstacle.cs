using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObstacle : MonoBehaviour
{
    public float damage = 1000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            if(col.GetComponent<Player>().isDashing == true)
            {
                Destroy(gameObject);
            }
            else
            {
                // Damage player if not dashing
                col.GetComponent<Player>().addDamage(damage);
            }

        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            if (col.GetComponent<Player>().isDashing == true)
            {
                Destroy(gameObject);
            }
        }
    }
}
