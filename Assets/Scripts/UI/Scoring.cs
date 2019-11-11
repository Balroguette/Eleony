using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    private Interactable target;
    public Image destroyBar;
    public Image scoreBar;

    /* public Scoring score; // créer un objet correspondant à scoring
     int progress;*/
    float temps;

    private void Awake()
    {
        target = this.GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update() {
        HandleDestroyBar();
        HandleScoreBar();
    }

    private void HandleDestroyBar()
    {
        if (destroyBar != null && target.gameObject.activeInHierarchy == true)
        {
            destroyBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position) + new Vector3(0f, 30f, 0f);//30 = offset

            temps = (Time.time - target.lastInteractTime) / target.destroyCooldown; //time = donne chiffre entre 0-1
                                                                                    //progress += 1; 
            if (target.hasBeenUsed == true && temps <= 1) //lorsque presque tout le temps estpassé
            {
                destroyBar.fillAmount = 1 - temps; // 1- =vider la barre petit à petit
            }
        }
    }

    private void HandleScoreBar()
    {
        if (target.hasBeenUsed == true)
        {
            scoreBar.gameObject.SetActive(true);
            if(scoreBar.transform.parent.GetComponent<Image>() == null)
            {
                scoreBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position) + new Vector3(0f, 30f, 0f);//30 = offset
            }
            if (target.gameObject.activeInHierarchy == true)
            {
                temps = (Time.time - target.lastInteractTime) / target.interactCooldown;
                
                scoreBar.fillAmount = 1 - temps;
                scoreBar.color = new Vector4(1 - temps, 1, 1 - temps, 1);
            }
        }
        else
        {
            scoreBar.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (destroyBar != null)
        {
            destroyBar.gameObject.SetActive(true);
        }
        if (scoreBar != null)
        {
            scoreBar.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (destroyBar != null)
        {
            destroyBar.gameObject.SetActive(false);
        }
        if (scoreBar != null)
        {
            scoreBar.gameObject.SetActive(false);
        }
    }

    public void DeleteBar() //quand arrive à 0 supprime visuellement la barre qu'il reste
    {
        destroyBar.fillAmount = 0;
    }

}
