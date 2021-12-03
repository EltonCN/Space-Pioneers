using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20f;
    [SerializeField] Vector3 rotationAxis = new Vector3(0, 1, 0 );

    [SerializeField] bool randomRotation = false;
    [SerializeField] float minRotation = 20f;
    [SerializeField] float maxRotation = 20f;


    [SerializeField] bool randomAxis = false;

    
    void Start()
    {
        if(randomRotation)
        {
            rotationSpeed = Random.Range(minRotation, maxRotation);
        }

        if(randomAxis)
        {
            float x = Random.Range(0f, 1f);
            float y = Random.Range(0f, 1f);
            float z = Random.Range(0f, 1f);

            rotationAxis = new Vector3(x, y, z);
        }
        
        rotationAxis.Normalize();
    }

    void Update()
    {
        this.transform.Rotate(rotationAxis*rotationSpeed*Time.deltaTime);
    }

    void OnValidate()
    {
        rotationAxis.Normalize();
    }
}
