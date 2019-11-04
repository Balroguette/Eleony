using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    /*
    [SerializeField] private GameObject PointA; // "serialzeField" sert à modifier les variables dans l'editeur / inspector unity
    [SerializeField] private GameObject PointB; // "private" restreint l'acces de ces variables à ce script
    [SerializeField] private GameObject PointC;
    */
    [SerializeField,Range(0,20)] private float speed; // "range" mets en place un slider dans l'inspector
    
    [SerializeField] string boostTag;

    //private Rigidbody controller; //tuto

    public float boostSpeed;
    public float boostCooldown;
    public float lastBoost;
    Vector3 direction;

    void Start()
    {
        lastBoost = 0; //dispo dès le début
    }
    void FixedUpdate() 
    {
        float AxisX = Input.GetAxis("Horizontal"); //je récupère la valeur de l'axe horizontal dans axisX (en float) pour pouvoir l'utiliser pour la rajouter a vector3
        float AxisZ= Input.GetAxis("Vertical");    //je récupère la valeur de l'axe vertical dans axisX (en float) pour pouvoir l'utiliser pour la rajouter a vector3
        direction = new Vector3(AxisX, 0, AxisZ);      // créé un v3 qui se base sur les  informations precedement recupérées (chiffres des inputs)
        this.transform.position = this.transform.position + direction * Time.deltaTime * speed;   
        
        // j'peux creer valeur speed mtn 


    }
    public void Boost() 
    {
        
        if (Time.time - lastBoost > boostCooldown) //ne pas spam le speed
        {
            Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
            Debug.Log("Boost function");
            rb.AddForce(direction.normalized * boostSpeed, ForceMode.VelocityChange); 
            //rb.velocity *= 2f;
            lastBoost = Time.time;
        }
    }
    public void OnTriggerEnter(Collider other)
    { 
        //LA PARTIE QUI BUG :
        /*if (other.gameObject.CompareTag(boostTag) && Input.GetKeyDown(KeyCode.Space))
        {
           Interactable interactableScript = other.gameObject.GetComponent<Interactable>();
           interactableScript.Boost(); //lance la fonction Boost du scriptinteractable
        }*/
    }


}

