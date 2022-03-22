using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObstacle : MonoBehaviour
{
    public float damage = 1000;


    public AK.Wwise.Event destroySound;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            if(col.GetComponent<Player>().isDashing == true)
            {
                Destroy(gameObject);
                if(destroySound!= null) destroySound.Post(gameObject);
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
                if (destroySound != null) destroySound.Post(gameObject);
            }
        }
    }
}
