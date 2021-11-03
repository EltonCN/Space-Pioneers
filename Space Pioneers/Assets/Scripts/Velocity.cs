using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[AddComponentMenu("SpacePioneers/Velocity")]
[RequireComponent(typeof(Rigidbody))]
public class Velocity : MonoBehaviour
{
    [SerializeField] private Vector3 initialVelocity;
    private Rigidbody rb;

    [SerializeField] private Vector3 velocity;
    public Vector3 ActualVelocity
    {
        get
        {
            return this.velocity;
        }
        set
        {
            ChangeVelocity(value);
        }
    }

    private void ChangeVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
        this.velocity = velocity;
    }

    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();

        ChangeVelocity(initialVelocity);
    }

    void Update()
    {
        this.velocity = rb.velocity;
    }
}
