using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    private Interactable target;
    public Image bar;

    /* public Scoring score; // créer un objet correspondant à scoring
     int progress;*/
    float temps;
    private void Start() {
        target = this.GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update() {

        if(target.gameObject.activeInHierarchy == true) {
            if(bar.gameObject.activeInHierarchy == false) {
                bar.gameObject.SetActive(true);
            }
            bar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position) + new Vector3(0f, 30f, 0f);
        } else {
            if (bar.gameObject.activeInHierarchy == true) {
                bar.gameObject.SetActive(false);
            }
        }

        temps = (Time.time - target.lastInteractTime) / target.destroyCooldown; //time = donne chiffre entre 0-1
        //progress += 1; 
        if (target.hasBeenUsed == true && temps <= 1) //lorsque presque tout le temps estpassé
        {
            
            bar.fillAmount = 1 - temps; // 1- =vider la barre petit à petit
        }
    }

    public void DeleteBar() //quand arrive à 0 supprime visuellement la barre qu'il reste
    {
        bar.fillAmount = 0;
    }

}
