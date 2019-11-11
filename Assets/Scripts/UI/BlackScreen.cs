using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    [SerializeField] private Interactable target;
    private Image imgScreen;
  
    void Start()
    {
        imgScreen = this.GetComponent<Image>();
    }
   
    void Update()
    {
        // active l'écran noir (qui en fait est blanc maintenant) après hasBeenUsed
        if (target.hasBeenUsed == true) 
        {
            imgScreen.color = new Vector4(1, 1, 1, (Time.time - target.lastInteractTime) / target.destroyCooldown);
        }
    }
} 
