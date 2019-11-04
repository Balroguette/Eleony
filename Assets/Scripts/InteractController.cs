using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractController : MonoBehaviour
{
    [SerializeField] string interactTag;
    public Text scorePoints; //nom doit être different du script, sert à afficher à l'ecran
    public int numbers; //la valeur que je vais stoquer

    void Start()
    {
        numbers = 0; //le score est à zéro
    }

    void Update()
    {
        scorePoints.text = numbers + "$";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(interactTag) && Input.GetKeyDown(KeyCode.Space))
        {
            //Récupération de la référence du script Interactable attaché sur l'objet du collider
            Interactable interactableScript = other.gameObject.GetComponent<Interactable>();

            interactableScript.Interact();

            if (interactableScript.ScoreCooldownIsUp())
            {
                numbers = numbers + interactableScript.scoreValue;
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
