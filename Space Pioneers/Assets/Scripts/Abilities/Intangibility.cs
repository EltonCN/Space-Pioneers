using UnityEngine;

[CreateAssetMenu]
public class Intangibility : Ability
{
    MeshRenderer shield;
    MeshCollider spaceship;
    Gravity spaceshipGravity;
    public override void Activate(GameObject parent)
    {
        shield = parent.GetComponent<MeshRenderer>();
        spaceship = parent.GetComponentInParent<MeshCollider>();
        spaceshipGravity = parent.transform.parent.GetComponentInParent<Gravity>();
        shield.enabled = true;
        spaceship.enabled = false;
        spaceshipGravity.enabled = false;
    }

    public override void BeginCooldown(GameObject parent)
    {
        shield = parent.GetComponent<MeshRenderer>();
        parent.GetComponentInParent<MeshCollider>();
        spaceshipGravity = parent.transform.parent.GetComponentInParent<Gravity>();
        shield.enabled = false;
        spaceship.enabled = true;
        spaceshipGravity.enabled = true;
    }
}
