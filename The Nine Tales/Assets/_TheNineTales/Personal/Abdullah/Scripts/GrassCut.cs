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

    public AK.Wwise.Event cutSound;
    public GameObject cutVFX;

    private Animator an;
    private void Awake()
    {
        if (destroyedPersistants == null)
            destroyedPersistants = new List<int>();

        an = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (persistant && destroyedPersistants.Contains(id))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        CheckCut(col);
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        CheckCut(col);
    }

    public void CheckCut(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            if (col.GetComponent<Player>().isDashing == true)
            {
                //Add grass to the inventory
                Player.inventory.AddItem(GrassCuttable);
                if (persistant) destroyedPersistants.Add(id);

                //Play cutting effects
                cutSound.Post(gameObject);
                Instantiate(cutVFX, transform.position + new Vector3(0, Random.Range(0.4f, 1f), 0), Quaternion.identity);

                //If there's an animator, trigger the destroy animation.
                if (an != null) an.SetTrigger("Dest");
                else Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Used by the animator to destroy the game object.
    /// </summary>
    public void RemoveGrass()
    {
        Destroy(gameObject);
    }
}
