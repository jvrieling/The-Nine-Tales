using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOffHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject fallOffFX;

    public void TriggerVFX()
    {
        if (fallOffFX)
        {
            GameObject temp = Instantiate(fallOffFX, transform.position, Quaternion.identity);

            Destroy(temp, 2);
        }
        else
            Debug.Log("Ask Edward where the fall off fx is");
    }
}
