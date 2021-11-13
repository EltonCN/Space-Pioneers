using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnter : MonoBehaviour
{
    [Tooltip("Object to wait enter in the collider.")] [SerializeField] GameObject targetObject;

    private void OnCollisionEnter(Collision other) 
    {
        if(targetObject == null || other.gameObject == targetObject)
        {
            Destroy(other.gameObject);
        }

        Debug.Log("Destruído");
    }
}
