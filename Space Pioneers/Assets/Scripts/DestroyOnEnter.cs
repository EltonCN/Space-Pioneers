using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnter : MonoBehaviour
{
    [Tooltip("Object to wait enter in the collider.")] [SerializeField] GameObject targetObject;


    private GameObject parent;

    void Awake()
    {
        parent = this.transform.parent.gameObject;
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(this.parent != null)
        {
            if(GameObject.ReferenceEquals(other.gameObject, this.parent))
            {
                return;
            }

            if(other.transform.IsChildOf(parent.transform))
            {
                return;
            }
        }
        else if(other.transform.IsChildOf(transform))
        {
            return;
        }
        

        if(targetObject == null || other.gameObject == targetObject)
        {
            Destroy(other.gameObject);
        }
    }
}
