using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isBooster;
    [SerializeField] bool isDestroyer;
    [SerializeField] protected bool countsAsDisabled;
    [SerializeField] private GameObject objectConcerned;
    [SerializeField] protected GameManager gameManager;
    [SerializeField] private GameObject FloatingTextPrefab;
    //SONS
    private AudioSource audioSource;
    [SerializeField] private AudioClip interactSound;

    protected SpriteRenderer sprRenderer;
    private Animator animator;
    public int scoreValue; //la valeur du score que donnent les differents objectifs
    public float destroyCooldown;
    [SerializeField, Range(0,1)] private float startShakingPercent;
    [HideInInspector] public float lastInteractTime; //garde le dernier time où joueur a interagit
    [HideInInspector] public bool hasBeenUsed;
    public float interactCooldown;
    private Vector3 startingPos;
    private Scoring classScore; //quand setactivfalse desactive bar 
    /*
    ===================================================================
    Pas besoin de mettre ce champ en SerializeField car il peut être récupéré dans le Start avec un GetComponent<Scoring> étant donné qu'il est attaché au même objet.
    Cela évite de devoir assigner manuellement chaque champs classScore d'un Interactable manuellement pour un script qui est attaché au même objet.
    ===================================================================
    */

    private void Start()
    {
        classScore = this.gameObject.GetComponent<Scoring>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        sprRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        animator = this.gameObject.GetComponent<Animator>();
        if(interactCooldown == 0f)
        {
            interactCooldown = destroyCooldown / 2;
        }
        Reset(false);
        lastInteractTime = Time.time - interactCooldown;
        startingPos = this.gameObject.transform.position;
    }

    protected virtual void OnEnable()
    {
        Reset(hasBeenUsed);
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
        if(hasBeenUsed == true && destroyCooldown > 0)
        {
            if ((1 - (Time.time - lastInteractTime) / destroyCooldown) <= startShakingPercent)
            {
                Shake();
            } //calcul pour lancer le shake de l'objet

            if (Time.time - lastInteractTime > destroyCooldown)
            {
                this.gameObject.SetActive(false);
                Reset(true);
            }
        }
    }
    private void Shake()
    {
        Vector3 newCoordinates = new Vector3(); //Shake le sprite
        float speed = 10f; //vitesse du shake
        float amount = .5f; //importance du deplacement du sprite

        speed = speed * ((Time.time - lastInteractTime) / destroyCooldown); //accélère le shake en fonction du temps restant

        newCoordinates.x = startingPos.x + Mathf.Sin((Time.time-lastInteractTime) * speed) * amount;
        newCoordinates.y = startingPos.y + Mathf.Sin((Time.time-lastInteractTime) * speed) * amount; //shake que sur l'axe y et x 
        newCoordinates.z = startingPos.z;

        gameObject.transform.position = newCoordinates;
    }//shake l'objet 

    private void TriggerCooldownAnim()
    {
        animator.SetTrigger("cooldown_up");
    } //active l'état dans l'animator 

    public void DelayCooldownAnim() //lance l'animation avec un délais
    {
        Invoke("TriggerCooldownAnim", interactCooldown);
    }

    private void Reset(bool used)
    {
        hasBeenUsed = used; //reset le hasBeenUsed
        lastInteractTime = Time.time; //pour pas avoir le cooldown qui se lance sans activation prealable de l'objet
    }

    public virtual void Interact()
    {
        audioSource.PlayOneShot(interactSound);
        //objectConcerned.SetActive(!isDestroyer);
        if(objectConcerned != null)
        {
            if (isDestroyer) {
                objectConcerned.SetActive(false);
                //gameManager.DisableObject(objectConcerned.GetComponent<Interactable>()); //récupération de la référence du script Interactable sur objectConcerned (qui est un gameObject)
            } else {
                objectConcerned.SetActive(true); //il faut que je dise que je crée un nouvel objet, et non pas accéder à l'ancien
                                                 //gameManager.AddObject(objectConcerned.GetComponent<Interactable>());
            }
        }
        gameManager.AddScore(scoreValue);
        DelayCooldownAnim();
        ShowFloatingText();

        lastInteractTime = Time.time;

        hasBeenUsed = true;
    }

    public virtual bool CanInteract() //vérifie si je peux interagir avec l'objet
    {
        //raccourci pour un if 
        return (Time.time - lastInteractTime > interactCooldown);
    }

    public void Boost()
    {
        
        if (isBooster)
        {
            objectConcerned.SetActive(false);
        } // je test le tag isBooster
        else
        {
            objectConcerned.SetActive(true);
        }

    } 

    public void ShowFloatingText() //Texte
    {
        if(scoreValue > 0) 
        {
            GameObject go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform); //go est le nom de ma variable de type GameObject, créer une référence vers l'objet que je viens d'instancier
            go.GetComponent<TextMesh>().text = scoreValue.ToString();  

            if (gameManager.scoreMultiplier > 1)
            {
                go = Instantiate(FloatingTextPrefab, transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity, transform);
                go.GetComponent<TextMesh>().text = "x" + gameManager.scoreMultiplier.ToString();
            }
        }
    }
    
}
