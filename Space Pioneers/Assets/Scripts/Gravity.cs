using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("SpacePioneers/Mechanics/Gravity")]
[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    [SerializeField] private GravityRS set;

    [SerializeField] private float range = 10f;
    [SerializeField] private bool showRange = false;

    private float gravityConstant  = 6.67408f;
    private bool frozen = false;

    Rigidbody ownRb;

    PhysicsScene scene;

    public GravityRS GravityRS
    {
        get
        {
            return this.set;
        }
        set
        {
            this.set.Remove(this);
            this.set = value;
            set.Add(this);
        }
    }
    void Start()
    {
        ownRb = GetComponent<Rigidbody>();
        scene = this.gameObject.scene.GetPhysicsScene();
    }

    void Awake()
    {
        set.Add(this);
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
                    rb.transform.rotation = Quaternion.LookRotation(rb.velocity, transform.up);
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


    void OnDestroy()
    {
        set.Remove(this);
    }

    [SerializeField] float rangeToFieldScale = 1.3285714285714285714285714285714f;

    void OnValidate()
    {
        Transform childTransform = transform.Find("GravityFieldEffect");
        
        if(childTransform != null)
        {
            float fieldRange = rangeToFieldScale*range;
            childTransform.localScale = new Vector3(fieldRange, fieldRange, fieldRange);
        }
    }
}