using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class Satelite : MonoBehaviour
{
    VisualEffect transmission;
    [SerializeField] GameObject targetObject;
    void Start()
    {
        transmission = GetComponent<VisualEffect>();
    }

    void Update()
    {
        Vector3 origin = transform.rotation * new Vector3(-0.023f,-0.0199999996f,-0.764999986f);
        origin += transform.position;

        transmission.SetVector3("Origin", origin);
        transmission.SetVector3("Target", targetObject.transform.position);
    }

    public void SendTransmission()
    {
        transmission.Play();
    }
}
