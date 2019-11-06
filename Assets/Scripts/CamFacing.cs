using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFacing : MonoBehaviour
{
    //Oriente sprite vers camera mouvement 
    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}