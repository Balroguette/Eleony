using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enum (énumération) détermine une liste de valeurs possibles et délimitée
public enum eEVENT { ENABLE, DISABLE, INTERACT};

public class Interactable_Chained : Interactable
{
    [SerializeField] private Interactable target;

    //Ces variables permettent un contrôle modulaire

    //Définit la nature de l'événement qui va déclencher l'action
    [SerializeField] private eEVENT eventToOperateOn; //Comme il y a plus de 2 valeurs possibles pour la variable on ne peut pas faire comme un booléen
    [SerializeField] private eEVENT actionOnTarget;//Définit l'action qui sera déclenchée sur l'objet target

    protected override void OnEnable()
    {
        base.OnEnable();
        if(eventToOperateOn == eEVENT.ENABLE)
        {
            HandleTarget();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (eventToOperateOn == eEVENT.DISABLE)
        {
            HandleTarget();
        }
    }

    public override void Interact() {
        base.Interact();
        if(eventToOperateOn == eEVENT.INTERACT)
        {
            HandleTarget();
        }
    }

    private void HandleTarget()
    {
        //switch case permet à remplacer une imbrication de "if else". Permet de tester un ensemble de valeurs possibles d'une variable (très utile avec les enum)
        switch (actionOnTarget) {
            case eEVENT.DISABLE:
                target.gameObject.SetActive(false);
                break;
            case eEVENT.ENABLE:
                target.gameObject.SetActive(true);
                break;
            case eEVENT.INTERACT:
                Debug.Log("Trigger Interact of " + target.name + " object");
                target.Interact();
                break;
        }
    }
}
