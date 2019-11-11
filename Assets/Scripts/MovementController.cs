using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    /*
    [SerializeField] private GameObject PointA; // "serialzeField" sert à modifier les variables dans l'editeur / inspector unity
    [SerializeField] private GameObject PointB; // "private" restreint l'acces de ces variables à ce script */

    // "range" mets en place un slider dans l'inspector
    [SerializeField,Range(0,20)] private float defaultSpeed; //Vitesse par défaut
    [SerializeField] private SpriteRenderer sprRenderer;
    //Boost de vitesse
    [SerializeField] float boostDuration;
    public float boostSpeed;
    public float boostCooldown;
    public float lastBoost;
    Vector3 direction;
    ParticleSystem boosted;

    

    private float currentSpeed; //Vitesse actuelle

    void Start()
    {
        boosted = this.GetComponent<ParticleSystem>(); //signe d'activation du boostspeed
        lastBoost = 0; //dispo dès le début
        currentSpeed = defaultSpeed; //vitesse de base au debut
    }

    void FixedUpdate() 
    {
        float AxisX = Input.GetAxis("Horizontal"); //je récupère la valeur de l'axe horizontal dans axisX (en float) pour pouvoir l'utiliser pour la rajouter a vector3
        float AxisZ= Input.GetAxis("Vertical");    //je récupère la valeur de l'axe vertical dans axisX (en float) pour pouvoir l'utiliser pour la rajouter a vector3
        direction = new Vector3(AxisX, 0, AxisZ);      // créé un v3 qui se base sur les  informations precedement recupérées (chiffres des inputs)
        this.transform.position = this.transform.position + direction * Time.deltaTime * currentSpeed;

        // tourne le sprite en fonction de la direction
        if (AxisX > 0) { 
            sprRenderer.flipX = false;
        } else if(AxisX < 0) { 
            sprRenderer.flipX = true;
        }
    }

    private void ResetSpeed() //Reset le SpeedBoost
    {
        currentSpeed = defaultSpeed;
        boosted.Stop();
    }

    public void Boost() //SpeedBoost
    {
        if (Time.time - lastBoost > boostCooldown) //ne pas spam le speed
        {
            Debug.Log("Boost activated");
            boosted.Play();
            currentSpeed += boostSpeed;
            lastBoost = Time.time;
            Invoke("ResetSpeed", boostDuration);//Appel délayé avec la durée de "boostDuration" de la fonction ResetSpeed
            //rb.AddForce(direction.normalized * boostSpeed, ForceMode.VelocityChange); 
            //rb.velocity *= 2f;
        }
    }
}

