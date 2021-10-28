using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SpacePioneers/Mechanics/Frozen")]
[RequireComponent(typeof(Rigidbody))]
public class Frozen : MonoBehaviour
{
    public Vector3 velocity;
    private Rigidbody rb;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    public void OnDisable()
    {
        rb.isKinematic = false;
        rb.detectCollisions = true;

        if(this.velocity != null)
        {
            rb.velocity = this.velocity;
        }
    }

    public void OnEnable()
    {
        this.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

        rb.isKinematic = true;
        //rb.detectCollisions = false;
    }


}
