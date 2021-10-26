using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


[AddComponentMenu("SpacePioneers/Mechanics/Gravity")]
[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    [SerializeField] private GravityRS set;

    [SerializeField] private float range = 10f;
    [SerializeField] private bool showRange = false;
    [SerializeField] public Vector3 v0 = new Vector3(0f, 0f, 0f);

    private float gravityConstant  = 6.67408f;
    private bool frozen = false;

    Rigidbody ownRb;

    PhysicsScene scene;


    void Start()
    {
        ownRb = GetComponent<Rigidbody>();
        ownRb.velocity = v0;

        scene = this.gameObject.scene.GetPhysicsScene();
    }
 
    void FixedUpdate()
    {
        if(frozen)
        {
            return;   
        }

        gravityRun(scene);
    }


    public void gravityRun(PhysicsScene scene)
    {
        if(ownRb == null)
        {
            ownRb = GetComponent<Rigidbody>();
            ownRb.velocity = v0;
        }

        List<Rigidbody> rbs = new List<Rigidbody>();

        foreach(Gravity g in set.Items)
        {
            Rigidbody rb = g.GetComponent<Rigidbody>();

            if (rb != null && rb != ownRb && !rbs.Contains(rb))
            {
                rbs.Add(rb);
                Vector3 offset = transform.position - rb.transform.position;

                if(offset.magnitude > range)
                {
                    continue;
                }
                
                Vector3 dir = offset.normalized;

                Vector3 force = (gravityConstant* dir* ownRb.mass * rb.mass) / offset.sqrMagnitude;

                if (!float.IsNaN(force.x) && !float.IsNaN(force.y) && !float.IsNaN(force.z))
                {
                    rb.AddForce(force);
                }
                
            }
        }
    }
 
    void OnDrawGizmos()
    {
        if(showRange)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }

    void OnEnable()
    {
        set.Add(this);
    }

    void OnDisable()
    {
        set.Remove(this);
    }
}