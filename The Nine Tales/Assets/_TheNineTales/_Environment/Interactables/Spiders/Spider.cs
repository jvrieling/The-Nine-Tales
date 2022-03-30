using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public Sprite deadSprite;
    public Vector3 launchForceMin, launchForceMax;
    public float launchTorqueMin, launchTorqueMax;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private DestroyAfterDelay dad;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        dad = GetComponent<DestroyAfterDelay>();
    }

    public void Kill()
    {
        dad.enabled = true;
        rb.isKinematic = false;
        sr.sprite = deadSprite;
        rb.AddForce(RandomVector(launchForceMin, launchForceMax), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(launchTorqueMin, launchTorqueMax));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestructableObstacle temp = collision.gameObject.GetComponent<DestructableObstacle>();
        if(temp != null)
        {
            temp.spider = this;
        }
    }
    public static Vector3 RandomVector(Vector3 min, Vector3 max)
    {
        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
    }
}
