using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseEvent : MonoBehaviour
{
    [SerializeField] GameEvent raiseOnEnable;
    [SerializeField] GameEvent raiseOnDisable;
    [SerializeField] bool raiseEnableOnStart = true;

    void Start()
    {
        if(raiseEnableOnStart)
        {
            raiseOnEnable.Raise();
        }
    }

    void OnEnable()
    {
        raiseOnEnable.Raise();
    }

    void OnDisable()
    {
        raiseOnDisable.Raise();
    }
}
