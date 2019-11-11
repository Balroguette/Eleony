using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]//Permet d'utiliser l'attribut SerializeField sur une variable de ce type
public class SpawnElement
{
    public Interactable interactableToEnable;
    public int scoreCondition;
    [HideInInspector] public bool hasBeenEnabled;
}

[DefaultExecutionOrder(0)]
public class GameManager : MonoBehaviour
{
    //new List permet d'initialiser une variable de type List, sinon elle n'existe pas
    private List<Interactable> activatedInteractables = new List<Interactable>();
    private List<Interactable> disabledInteractables = new List<Interactable>();
    [SerializeField] private List<SpawnElement> elementsToSpawn;
    [SerializeField] private Interactable loseConditionObject;
    [HideInInspector] public int scoreMultiplier;
    private int totalScore; //Valeur du score actuel
    public Text scorePoints; //nom doit être different du script, sert à afficher à l'ecran

    private void Start()
    {
        ResetScoreMultiplier(); //reset le score multiplier
    }

    private void Update()
    {
        scorePoints.text = totalScore.ToString();
        //Vérification de l'état (actif ou inactif) de l'objet qui représente la condition de défaite
        if (loseConditionObject != null && loseConditionObject.hasBeenUsed && loseConditionObject.gameObject.activeInHierarchy == false)
        {
            GameOver();
        }
    }

    public void ResetScoreMultiplier()
    {
        scoreMultiplier = 1;
    }

    //Fonction d'incrémentation du score (appelée depuis InteractController) pour pouvoir le multiplier
    public void AddScore(int value)
    {
        totalScore += (value * scoreMultiplier); //Revient à écrire totalScore = totalScore + value * scoreMultiplier;
        for (int i = 0; i < elementsToSpawn.Count; i += 1)
        {
            if (totalScore >= elementsToSpawn[i].scoreCondition)
            {
                if (elementsToSpawn[i].hasBeenEnabled == false)
                {
                    elementsToSpawn[i].hasBeenEnabled = true;
                    elementsToSpawn[i].interactableToEnable.gameObject.SetActive(true);
                    AddObject(elementsToSpawn[i].interactableToEnable);
                }
            }
        }
    }

    public void AddObject(Interactable interactableToAdd)
    {
        //Vérification que l'objet n'est pas déjà contenu dans la liste
        if (activatedInteractables.Contains(interactableToAdd) == false)
        {
            if (disabledInteractables.Contains(interactableToAdd) == true)
            {
                disabledInteractables.Remove(interactableToAdd);
            }
            activatedInteractables.Add(interactableToAdd);
        }
    }

    //Fonction de desactivation des objets
    public void DisableObject(Interactable interactableDisabled)
    {
        if(activatedInteractables.Contains(interactableDisabled) == true)
        {
            //activatedInteractables.Remove(interactableDisabled);
            disabledInteractables.Add(interactableDisabled);

            if(disabledInteractables.Count == (activatedInteractables.Count / 2))//Count retourne le nombre d'objets dans la liste
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        print("Game over");
        //SceneManager.LoadScene(0);
        //TODO : Ecran Game Over
    }
}
