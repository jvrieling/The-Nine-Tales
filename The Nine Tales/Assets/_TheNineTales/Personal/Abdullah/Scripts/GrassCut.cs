using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassCut : MonoBehaviour
{
    public Item GrassCuttable;

    [Tooltip("Persistand objects will stay destroyed even if the player leaves the scene. Non-persistant ones will just reload with the scene.")]
    public bool persistant;
    public int id;

    private static List<int> destroyedPersistants;

    private void Awake()
    {
        if (destroyedPersistants == null)
            destroyedPersistants = new List<int>();
    }

    private void OnEnable()
    {
        if(persistant && destroyedPersistants.Contains(id))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            if (col.GetComponent<Player>().isDashing == true)
            {
                Player.inventory.AddItem(GrassCuttable);
                 if (persistant) destroyedPersistants.Add(id);
                Destroy(gameObject);
            }

        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            if (col.GetComponent<Player>().isDashing == true)
            {
                Player.inventory.AddItem(GrassCuttable);
                if (persistant) destroyedPersistants.Add(id);
                Destroy(gameObject);
            }

        }
    }
}
