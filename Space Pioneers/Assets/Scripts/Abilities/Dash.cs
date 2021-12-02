using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    public float dashSpeed;
    Rigidbody rig;
    public GameObject BigFlame1;
    ParticleSystem flames1;
    public GameObject BigFlame2;
    ParticleSystem flames2;
    private PlayerInput playerInput;
    InputAction dash;
    public AudioSource dashSound;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        flames1 = BigFlame1.GetComponent<ParticleSystem>();
        flames2 = BigFlame2.GetComponent<ParticleSystem>();
        playerInput = GetComponent<PlayerInput>();
        InputActionMap map = playerInput.currentActionMap;
        dash = map.FindAction("Dash", true);
        dash.performed += Dashing;
    }

    private void Dashing(InputAction.CallbackContext context) 
    {
        dashSound.Play();
        flames1.Play();
        flames2.Play();
        rig.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
    }
}
