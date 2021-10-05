using UnityEngine;
using System.Collections.Generic;
 
[AddComponentMenu("SpacePioneers/Gravity/Gravity")]
[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    [SerializeField]
    private float range = 10f;
 
    private float gravityConstant  = 6.67408f;

    [SerializeField]
    private bool showRange = false;

    [SerializeField]
    private Vector3 v0 = new Vector3(0f, 0f, 0f);

    Rigidbody ownRb;
 
    void Awake()
    {
    }

    void Start()
    {
        ownRb = GetComponent<Rigidbody>();
        ownRb.velocity = v0;
    }
 
    void FixedUpdate()
    {
        gravityRun();
    }

    public void gravityRun()
    {
        if(ownRb == null)
        {
            ownRb = GetComponent<Rigidbody>();
        }

        Collider[] cols = Physics.OverlapSphere(transform.position, range);
        List<Rigidbody> rbs = new List<Rigidbody>();
 
        foreach (Collider c in cols)
        {
            Rigidbody rb = c.attachedRigidbody;
            if (rb != null && rb != ownRb && !rbs.Contains(rb)  && (rb.gameObject.scene.name != "SimulationTrajetory") )
            {
                rbs.Add(rb);
                Vector3 offset = transform.position - c.transform.position;
                
                Vector3 dir = offset.normalized;

                Vector3 force = (gravityConstant* dir* ownRb.mass * rb.mass) / offset.sqrMagnitude;

                if (!float.IsNaN(force.x) && !float.IsNaN(force.y) && !float.IsNaN(force.z) && force.magnitude < 1000f)
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
}