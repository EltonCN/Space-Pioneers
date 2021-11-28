using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(GameObject parent) {}
    //public virtual void Deactivate(GameObject parent) {}
    public virtual void BeginCooldown(GameObject parent) {}
}
