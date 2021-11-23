using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public int count;
    public string targetTag = "Player";

    private SpriteRenderer sr;

    private void OnValidate()
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>();

        sr.sprite = item.icon;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == targetTag)
        {
            Inventory inv = collision.gameObject.GetComponentInChildren<Inventory>();

            inv.AddItem(new ItemStack(item, count));
            Destroy(gameObject);
        }
    }
}
