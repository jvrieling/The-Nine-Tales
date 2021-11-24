using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScanRadar : MonoBehaviour
{
    [SerializeField] private float maxSize = 2f;
    [SerializeField] private Vector3 growthRate = new Vector3 (4f, 4f, 4f);

    private Collider2D[] interactableObjects;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Fox");
        if (player != null) {
            Debug.Log(player);
        }
        player.GetComponent<SpawnRadar>().scannerDeployed = true;
        StartCoroutine(ScaleScanner(growthRate, maxSize));
    }
    private IEnumerator ScaleScanner(Vector3 growthRate, float scanSize) {

        while (transform.localScale.x <= scanSize)
        {
            transform.localScale += (growthRate * Time.deltaTime);
            yield return null;
        }
        Debug.Log("It's going through the rest of the code each frame");
        interactableObjects = Physics2D.OverlapCircleAll((Vector2)transform.position, scanSize*2);
      //  OnDrawGizmos();
        foreach (Collider2D temp in interactableObjects)
        {
            if (temp.gameObject.tag == "Illusion")
            {
                temp.SendMessage("Reveal");
            }
        }
        player.SendMessage("RefreshAbility");
        Destroy(this.gameObject);

    }

   private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, maxSize * 2);
    }
}
