using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    [SerializeField] private Interactable target;
    private Image imgScreen;
  

    // Start is called before the first frame update
    void Start()
    {
        imgScreen = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
              
        if (target.hasBeenUsed == true) 
        {
            imgScreen.color = new Vector4(0, 0, 0, (Time.time - target.lastInteractTime) / target.destroyCooldown);
        }
    }
} 
