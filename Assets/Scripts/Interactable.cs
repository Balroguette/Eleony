using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public bool isBooster;
    [SerializeField] bool isDestroyer;
    [SerializeField] protected bool countsAsDisabled;

    [SerializeField] private GameObject objectConcerned;
    [SerializeField] protected GameManager gameManager;
    [SerializeField] private GameObject FloatingTextPrefab;
    protected SpriteRenderer sprRenderer;
    public int scoreValue; //la valeur du score que donnent les differents objectifs
    public float destroyCooldown;
    [HideInInspector] public float lastInteractTime; //garde le dernier time où joueur a interagit
    [HideInInspector] public bool hasBeenUsed;
    [SerializeField] private float scoreCooldown;
    [HideInInspector] public float lastScoreTime;

    /*
    ===================================================================
    Pas besoin de mettre ce champ en SerializeField car il peut être récupéré dans le Start avec un GetComponent<Scoring> étant donné qu'il est attaché au même objet.
    Cela évite de devoir assigner manuellement chaque champs classScore d'un Interactable manuellement pour un script qui est attaché au même objet.
    ===================================================================
    */
    private Scoring classScore; //quand setactivfalse desactive bar

    private void Start()
    {
        classScore = this.gameObject.GetComponent<Scoring>();
        sprRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        Reset();
        scoreCooldown = destroyCooldown / 2;
        lastScoreTime = Time.time - scoreCooldown; //pour pas avoir le cooldown qui se lance sans activation prealable de l'objet
    }

    protected virtual void OnEnable()
    {
        gameManager.AddObject(this);
    }

    protected virtual void OnDisable()
    {
        if (countsAsDisabled)
        {
            gameManager.DisableObject(this);
        }
    }

    private void Update()
    {
        if (destroyCooldown > 0 && hasBeenUsed == true && Time.time - lastInteractTime > destroyCooldown)
        {
            // ATTENTION JE MEURS
            classScore.DeleteBar();
            this.gameObject.SetActive(false);
            Reset();
        }
        
    }

    private void Reset()
    {
        hasBeenUsed = false; //reset le hasBeenUsed
        lastInteractTime = 0;
        lastScoreTime = 0;
    }

    public virtual void Interact()
    {

        //objectConcerned.SetActive(!isDestroyer);
        if (isDestroyer)
        {
            objectConcerned.SetActive(false);
            //gameManager.DisableObject(objectConcerned.GetComponent<Interactable>()); //récupération de la référence du script Interactable sur objectConcerned (qui est un gameObject)
        }
        else
        {
            objectConcerned.SetActive(true); //il faut que je dise que je crée un nouvel objet, et non pas accéder à l'ancien
            //gameManager.AddObject(objectConcerned.GetComponent<Interactable>());
        }

        lastInteractTime = Time.time;

        hasBeenUsed = true;
    }

    public virtual bool CanInteract()
    {
        return true;
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
        if(scoreValue > 0)
        {
            GameObject go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().text = scoreValue.ToString();

            if(gameManager.scoreMultiplier > 1)
            {
                go = Instantiate(FloatingTextPrefab, transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity, transform);
                go.GetComponent<TextMesh>().text = "x" + gameManager.scoreMultiplier.ToString();
            }
        }
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
