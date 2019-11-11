using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_ScoreBooster : Interactable
{
    [SerializeField] private int scoreMultiplier;
    [SerializeField] private float multiplierDuration;

    public override void Interact()
    {
        base.Interact(); //base appelle la premiere fonction Interact que j'ai faite, pas celle-ci 
        gameManager.scoreMultiplier = scoreMultiplier;
        Invoke("StopMultiplier", multiplierDuration); //Invoke appelle la fonction "StopMuliplier" en attendant x (multiplierDuration) temps
    }

    private void StopMultiplier()
    {
        gameManager.ResetScoreMultiplier();
    }
}
