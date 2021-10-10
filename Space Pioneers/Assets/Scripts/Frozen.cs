using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SpacePioneers/GameMode/Frozen")]
[RequireComponent(typeof(Rigidbody))]
public class Frozen : MonoBehaviour, GameModeSensitive
{
    private Vector3 velocity;
    private Rigidbody rb;

    [SerializeField] GameModeManager modeManager;
    bool subscribed = false;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(subscribed == false)
        {
            if(modeManager == null)
            {
                modeManager = GameModeManager.Instance;   
            }
            if(modeManager != null)
            {
                modeManager.subscribe(this);
                subscribed = true;
            }
        }
        
    }

    public void OnEnterActionMode()
    {
        rb.isKinematic = false;
        rb.detectCollisions = true;

        if(this.velocity != null)
        {
            rb.velocity = this.velocity;
        }
    }

    public void OnEnterPlanningMode()
    {
        this.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

        rb.isKinematic = true;
        rb.detectCollisions = false;
    }


}
