using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_ScoreBooster : Interactable
{
    [SerializeField] private int scoreMultiplier;
    [SerializeField] private float multiplierDuration;

    public override void Interact()
    {
        base.Interact();
        gameManager.scoreMultiplier = scoreMultiplier;
        //Invoke appelle la fonction "StopMuliplier" en attendant x (multiplierDuration) temps
        Invoke("StopMultiplier", multiplierDuration);
    }

    private void StopMultiplier()
    {
        gameManager.ResetScoreMultiplier();
    }
}
