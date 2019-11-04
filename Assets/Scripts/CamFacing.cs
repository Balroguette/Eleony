using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFacing : MonoBehaviour
{
    public Camera m_Camera;

    //Oriente sprite vers camera mouvement 
    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}