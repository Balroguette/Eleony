using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_LimitedUse : Interactable
{
    [SerializeField] private int nbMaxUse;
    [SerializeField] private Sprite sprWhenChanged;
    private int nbUse = 0;

    public override void Interact()
    {
        if(nbUse < nbMaxUse) {
            nbUse += 1;
            base.Interact();
        } else {
            //change le sprite
            sprRenderer.sprite = sprWhenChanged;
        }
    }

    public override bool CanInteract()
    {
        if(nbUse < nbMaxUse)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected override void OnEnable()
    {
        //Surchage vide de la fonction OnEnable de base pour ne pas s'inscrire dans la liste des objetrs activés dans le GameManager
        //ne va pas dans interactable = fait Rien
    }
}
