using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseEventOnEnter : MonoBehaviour
{
    [Tooltip("Object to wait enter in the collider.")] [SerializeField] GameObject targetObject;
    
    [SerializeField] GameEvent eventToRaise;

    [SerializeField] bool fixOther = true;

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject == targetObject)
        {
            if(eventToRaise != null)
            {
                eventToRaise.Raise();
            }

            if(fixOther)
            {
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
