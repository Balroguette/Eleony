using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_LimitedUse : Interactable
{
    [SerializeField] private int nbMaxUse;
    [SerializeField] private Sprite sprWhenChanged;
    private int nbUse = 0;

    public override void Interact() //interagit un nombre de fois limité 
    {
        nbUse += 1; //incrémente
        base.Interact(); //base appelle la premiere fonction Interact que j'ai faite, pas celle-ci
        if (nbUse == nbMaxUse) // si on atteint la limite
        {
            //change le sprite
            sprRenderer.sprite = sprWhenChanged;
        }
    }

    public override bool CanInteract() //vérification
    {
        return (base.CanInteract() && nbUse < nbMaxUse);
    }

    protected override void OnEnable()
    {
        //Surchage vide de la fonction OnEnable de base pour ne pas s'inscrire dans la liste des objetrs activés dans le GameManager
        //ne va pas dans interactable = fait Rien
    }
}
