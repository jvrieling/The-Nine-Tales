using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObstacle : MonoBehaviour
{
    public float damage = 1000;

    public GameObject destroyedPrefab;
    public Spider spider;
    public AK.Wwise.Event destroySound;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            if(col.GetComponent<Player>().isDashing == true)
            {
                if (destroySound != null) destroySound.Post(gameObject);
                if (destroyedPrefab != null) Instantiate(destroyedPrefab, transform.position, Quaternion.identity);
                if (spider != null) spider.Kill();
                Destroy(gameObject);
                
            }
            else
            {
                // Damage player if not dashing
                col.GetComponent<Player>().addDamage(damage);
            }

        }
    }
}
