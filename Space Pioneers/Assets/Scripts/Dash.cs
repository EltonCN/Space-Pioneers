using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashSpeed;
    Rigidbody rig;
    bool isDashing;
    //ParticleSystem[] flames;
    public GameObject BigFlame1;
    ParticleSystem flames1;
    public GameObject BigFlame2;
    ParticleSystem flames2;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        //flames = GetComponents<ParticleSystem>();
        flames1 = BigFlame1.GetComponent<ParticleSystem>();
        flames2 = BigFlame2.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            isDashing = true;   
    
    }

    private void FixedUpdate() 
    {
        if (isDashing)
            Dashing();
    
    }

    private void Dashing() 
    {
        //flames[2].Play();
        //flames[3].Play();
        flames1.Play();
        flames2.Play();
        rig.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
        isDashing = false;
    }
}
