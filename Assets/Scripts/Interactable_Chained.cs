using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Chained : Interactable
{
    [SerializeField] private Interactable target;

    //Ces variables permettent un contrôle modulaire
    [SerializeField] private bool operateOnDisable;//Définit si l'action doit se faire au moment de la désactivation de l'objet
    [SerializeField] private bool disableTarget;//Définit si l'objet cible doit être désactivé ou activé

    protected override void OnEnable()
    {
        base.OnEnable();
        if(operateOnDisable == false)
        {
            HandleTarget();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (operateOnDisable == true)
        {
            HandleTarget();
        }
    }

    private void HandleTarget()
    {
        if (disableTarget == true)
        {
            target.gameObject.SetActive(false);
        }
        else
        {
            target.gameObject.SetActive(true);
        }
    }
}
