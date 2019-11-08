using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 3.0f;
    public Vector3 Offset = new Vector3(0, 2, 0); //pour plus de visibilité du txt
    public Vector3 RandomizeIntensity = new Vector3(1f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<MeshRenderer>().sortingLayerName = "Foreground";
        transform.Rotate(new Vector3(90f, 0f, 0f));
        Destroy (gameObject, DestroyTime); //détruire le text après son aparition avec le temps

        //transform.localPosition += Offset; //positionner avec offset
        /*transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
            Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
            Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));*/


    }
}
