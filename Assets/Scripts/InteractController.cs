using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    [SerializeField] string interactTag;
    [SerializeField] GameManager gameManager;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(interactTag) && Input.GetKeyDown(KeyCode.Space))
        {
            //Récupération de la référence du script Interactable attaché sur l'objet du collider
            Interactable interactableScript = other.gameObject.GetComponent<Interactable>();

            interactableScript.Interact();

            if (interactableScript.CanInteract() == true && interactableScript.ScoreCooldownIsUp() == true)
            {
                gameManager.AddScore(interactableScript.scoreValue);
                interactableScript.lastScoreTime = Time.time;

                interactableScript.ShowFloatingText();
            }

            if (interactableScript.isBooster) 
            {
                MovementController movementScript = this.gameObject.GetComponent<MovementController>();
                movementScript.Boost();
            }
        }
    }
    
}
