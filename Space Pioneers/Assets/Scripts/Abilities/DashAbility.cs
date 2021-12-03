using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability
{
    public float dashSpeed;
    Rigidbody rig;
    bool isDashing;
    ParticleSystem ps;
    GameObject spaceShip;
    public Vector3 savedVelocity;

    // Parent = flames
    public override void Activate(GameObject parent)
    {
        spaceShip = parent.transform.parent.gameObject;
        rig = spaceShip.GetComponent<Rigidbody>();
        
        foreach (Transform child in parent.transform)
        {
            ps = child.GetComponent<ParticleSystem>();
            var main = ps.main;
            main.duration = activeTime;
            ps.Play();
            savedVelocity = rig.velocity;
            //rig.velocity = new Vector3(rig.velocity.x);
            rig.AddForce(spaceShip.transform.forward * dashSpeed, ForceMode.Impulse);
        }
    }

    public override void BeginCooldown(GameObject parent)
    {
        ps.Stop();
    }
}
