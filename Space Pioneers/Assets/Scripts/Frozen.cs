using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SpacePioneers/Mechanics/Frozen")]
[RequireComponent(typeof(Velocity))]
public class Frozen : MonoBehaviour
{
    public Vector3 velocity;
    private Rigidbody rb;
    private Velocity vl;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        vl = this.GetComponent<Velocity>();
    }

    public void OnDisable()
    {
        rb.isKinematic = false;
        rb.detectCollisions = true;

        if(this.velocity != null)
        {
            rb.velocity = this.velocity;
        }

        vl.enabled = true;
    }

    public void OnEnable()
    {
        this.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

        vl.enabled = false;

        rb.isKinematic = true;
        //rb.detectCollisions = false;
    }


}
