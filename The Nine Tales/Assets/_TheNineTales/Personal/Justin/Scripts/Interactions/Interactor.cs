using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public float interactRange;
    public GameObject interactPrompt;
    public Text interactNameDisplay;
    public LayerMask interactLayers = 1 << 3;

    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, interactRange, Vector2.zero, 1, interactLayers);

        if (hit)
        {
            InteractableObject inter = hit.collider.gameObject.GetComponent<InteractableObject>();
            if (inter != null)
            {
                interactNameDisplay.text = inter.interactionName;
                interactPrompt.SetActive(inter.CanInteract());

                if (Input.GetKeyDown(KeyCode.E))
                {
                    inter.Interact(this);
                }
            } else
            {
                Debug.LogError(gameObject.name + " is on the 'Interactable' layer but does not have an Interactable Object script attached!");
            }
        }
        else
        {
            interactPrompt.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
