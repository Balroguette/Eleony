using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public bool isBooster;
    [SerializeField] bool isDestroyer;
    [SerializeField] GameObject objectConcerned;
    public GameObject FloatingTextPrefab;
    public int scoreValue; //la valeur du score que donnent les differents objectifs
    public float destroyCooldown;
    float lastInteractTime; //garde le dernier time où joueur a interagit
    private bool hasBeenUsed;
    float scoreCooldown;
    [HideInInspector] public float lastScoreTime;

    private void Start()
    {
        hasBeenUsed = false;
        scoreCooldown = destroyCooldown / 2;
    }

    private void Update()
    {
        if (destroyCooldown > 0 && hasBeenUsed == true && Time.time - lastInteractTime > destroyCooldown)
        {
            gameObject.SetActive(false);
            hasBeenUsed = false;
            lastInteractTime = 0;
            lastScoreTime = 0;
        }
        
    }
    public void Interact()
    {

        //objectConcerned.SetActive(!isDestroyer);
        if (isDestroyer)
        {
            objectConcerned.SetActive(false);
        }
        else
        {
            objectConcerned.SetActive(true); //il faut que je dise que je crée un nouvel objet, et non pas accéder à l'ancien
        }

        lastInteractTime = Time.time;

        hasBeenUsed = true;
    }
    public void Boost()
    {
        // je test le tag isBooster
        if (isBooster)
        {
            objectConcerned.SetActive(false);
        }
        else
        {
            objectConcerned.SetActive(true);
        }

    }
    public void ShowFloatingText()
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = scoreValue.ToString();
    }
    public bool ScoreCooldownIsUp()
    {
        if (Time.time - lastScoreTime > scoreCooldown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    
}
