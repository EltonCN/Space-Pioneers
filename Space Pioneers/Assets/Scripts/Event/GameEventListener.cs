// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("SpacePioneers/Game Event/Game Event Listener")]
public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent Response;

    [SerializeField] float delay = 0;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        if(gameObject.activeInHierarchy)
        {
            StartCoroutine("RaiseResponse");
        }
        
    }

    IEnumerator RaiseResponse()
    {
        if(delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }
        
        Response.Invoke();
    }
}