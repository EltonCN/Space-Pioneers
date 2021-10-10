using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SpacePioneers/GameMode/Frozen")]
[RequireComponent(typeof(Rigidbody))]
public class Frozen : MonoBehaviour, GameModeSensitive
{
    public Vector3 velocity;
    private Rigidbody rb;

    [SerializeField] public GameModeManager modeManager;
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
        //rb.constraints = RigidbodyConstraints.FreezePositionY;

        if(this.velocity != null && subscribed)
        {
            rb.velocity = this.velocity;
        }
    }

    public void OnEnterPlanningMode()
    {
        this.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

        rb.isKinematic = true;
        //rb.constraints = RigidbodyConstraints.FreezeAll;
    }


}
