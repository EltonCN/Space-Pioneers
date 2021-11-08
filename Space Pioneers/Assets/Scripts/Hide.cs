using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SpacePioneers/Mechanics/Hide")]
public class Hide : MonoBehaviour
{
    Renderer[] renderers;

    bool[] previousStates;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        previousStates = new bool[renderers.Length];
    }

    void OnEnable()
    {
        for(int i = 0; i<renderers.Length; i++)
        {
            if(renderers[i] != null)
            {
                previousStates[i] = renderers[i].enabled;
                renderers[i].enabled = false;
            }
        }
        
    }

    void OnDisable()
    {
        for(int i = 0; i<renderers.Length; i++)
        {
            if(renderers[i] != null)
            {
                renderers[i].enabled = previousStates[i] ;
            }
        }
    }
}
